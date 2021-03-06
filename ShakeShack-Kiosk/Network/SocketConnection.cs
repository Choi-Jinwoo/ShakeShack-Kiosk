﻿using Newtonsoft.Json;
using ShakeShack_Kiosk.Enum;
using ShakeShack_Kiosk.Model;
using ShakeShack_Kiosk.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShakeShack_Kiosk.Network
{
    static class Constants
    {
        public const string SERVER_IP = "10.80.162.152";
        public const int PORT = 80;
        public const int BUFFER_SIZE = 1024;
    }
    public class SocketConnection : INotifyPropertyChanged
    {
        private static SocketConnection instance;
        public TcpClient Client { get; private set; }
        private Thread receiveThread { get; set; }
        private bool isConnected { get; set; }
        private bool isSendResult { get; set; } = false;
        private DateTime lastConnectedDt { get; set; }
        public DateTime LastConnectedDt
        {
            get => lastConnectedDt;
            set
            {
                lastConnectedDt = value;
                OnPropertyChanged("LastConnectedDt");
            }
        }

        public bool IsConnected
        {
            get => isConnected;
            set
            {
                isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }

        private SocketConnection() { }

        public static SocketConnection Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new SocketConnection();
                }
                return instance;
            }
        }

        public void Connect()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Constants.SERVER_IP), Constants.PORT);
            Client = new TcpClient();

            try
            {
                Client.Connect(ep);
                IsConnected = true;

                MsgPacket packet = new MsgPacket()
                {
                    MSGType = (int)MSGTypeEnum.LOGIN,
                    Id = "2119"
                };

                SendMessage(packet);

                receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
                LastConnectedDt = DateTime.Now;
            } catch (Exception e)
            {
                IsConnected = false;
                Debug.WriteLine(e);
            }

        }

        public void SendMessage(MsgPacket packet)
        {
            Task sendMessageTask = new Task(() =>
            {
                try
                {
                    string strJson = JsonConvert.SerializeObject(packet);
                    byte[] byteData = Encoding.UTF8.GetBytes(strJson);

                    NetworkStream networkStream = Client.GetStream();
                    networkStream.Write(byteData, 0, byteData.Length);
                    isSendResult = true;
                }
                catch (Exception e)
                {
                    IsConnected = false;
                    Debug.WriteLine(e);
                }
            });

            sendMessageTask.Start();
        }

        public void ReceiveMessage()
        {
            try
            {
                while (true)
                {
                    NetworkStream networkStream = Client.GetStream();
                    byte[] buffer = new byte[Constants.BUFFER_SIZE];
                    int length = networkStream.Read(buffer, 0, buffer.Length);

                    if (length <= 0)
                    {
                        IsConnected = false;
                        receiveThread.Abort();
                        LastConnectedDt = DateTime.Now;
                        break;
                    }

                    string response = Encoding.UTF8.GetString(buffer, 0, length);

                    if (isSendResult == true)
                    {
                        isSendResult = false;
                        continue;
                    }

                    if (response.Contains("총매출액"))
                    {
                        MsgPacket msgPacket = new MsgPacket()
                        {
                            MSGType = 1,
                            Group = true,
                            Id = "2119",
                            Content = $"총 매출액 : {OrderHistoryViewModel.Instance.TotalSales.ToString()}"
                        };
                        SendMessage(msgPacket);
                        continue;
                    }

                    MessageBox.Show(response);
                }
            } catch (Exception e)
            {
                IsConnected = false;
                Debug.WriteLine(e);
                receiveThread.Abort();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

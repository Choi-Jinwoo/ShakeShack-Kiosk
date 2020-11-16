using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ShakeShack_Kiosk.Connection
{
    class SocketConnection : INotifyPropertyChanged
    {
        private static SocketConnection instance;
        public Socket Sock { get; private set; }
        private bool isConnected;
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
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Sock = sock;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("10.80.162.152"), 80);
            Sock.Connect(ep);
            IsConnected = true;

            Thread thread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    if (Sock.Connected == false)
                    {
                        SocketConnection.Instance.IsConnected = false;
                        break;
                    }
                }
            }));
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

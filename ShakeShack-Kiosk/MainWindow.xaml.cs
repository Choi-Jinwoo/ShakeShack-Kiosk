using Newtonsoft.Json;
using ShakeShack_Kiosk.Connection;
using ShakeShack_Kiosk.Controls;
using ShakeShack_Kiosk.Enum;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShakeShack_Kiosk
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            titleControl.OnNavigateToHome += TitleControl_OnNavigateToHome; ;

            SocketConnection socketCon = SocketConnection.Instance;
            socketCon.Connect();

            MsgPacket packet = new MsgPacket()
            {
                MSGType = (int)MSGTypeEnum.LOGIN,
                Id = "2119"
            };

            socketCon.SendMessage(packet);
        }


        private void TitleControl_OnNavigateToHome(object sender, EventArgs e)
        {
            while (frPage.CanGoBack)
            {
                frPage.GoBack();
            }
        }
    }
}

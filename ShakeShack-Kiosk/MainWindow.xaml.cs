using Newtonsoft.Json;
using ShakeShack_Kiosk.Network;
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

            new Task(() =>
            {
                SocketConnection.Instance.Connect();
            }).Start();
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

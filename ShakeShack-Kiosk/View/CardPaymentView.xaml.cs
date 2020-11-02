using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ShakeShack_Kiosk.View
{
    /// <summary>
    /// ScanQRcode.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CardPaymentView : Page
    {
        public CardPaymentView()
        {
            InitializeComponent();
            webcam.CameraIndex = 0;
        }

        private void webcam_QrDecoded(object sender, string e) {
            // TODO: 결제 완료
            tbRecog.Text = e; 
        }
    }
}

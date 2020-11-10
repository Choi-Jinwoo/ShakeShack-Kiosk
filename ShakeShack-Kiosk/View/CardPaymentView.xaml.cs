using ShakeShack_Kiosk.ViewModel;
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
        private PaymentViewModel paymentViewModel = PaymentViewModel.Instance;
        private OrderFoodViewModel orderFoodViewModel = OrderFoodViewModel.Instance;
        public CardPaymentView()
        {
            InitializeComponent();
            this.DataContext = paymentViewModel;
            webcam.CameraIndex = 0;
            Loaded += CardPaymentView_Loaded;
        }

        private void CardPaymentView_Loaded(object sender, RoutedEventArgs e)
        {
            tbOrderFoodTotalPrice.Text = string.Format("총 가격 : {0}원", orderFoodViewModel.OrderFoodTotalPrice);
        }

        private void webcam_QrDecoded(object sender, string e) {
            paymentViewModel.SetPayUser(e);

            if (paymentViewModel.OrderUser == null)
            {
                tbQRDecodedStatus.Text = "존재하지 않는 회원";
            }
            else
            {
                tbQRDecodedStatus.Text = "";
            }
        }

        private void btnPrePage_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void btnResultPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            if (paymentViewModel.OrderUser == null)
            {
                MessageBox.Show("QR코드를 인식해주세요");
                return;
            }

            paymentViewModel.PayByCard();
            NavigationService.Navigate(new Uri("/View/ResultPaymentView.xaml", UriKind.Relative));
        }
    }
}

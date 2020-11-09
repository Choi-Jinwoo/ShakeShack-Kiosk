using ShakeShack_Kiosk.Database.Dao;
using ShakeShack_Kiosk.Model;
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
    /// CashPaymentView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CashPaymentView : Page
    {
        private PaymentViewModel paymentViewModel = PaymentViewModel.Instance;
        public CashPaymentView()
        {
            InitializeComponent();
            this.DataContext = paymentViewModel;
            Loaded += CashPaymentView_Loaded;
        }

        private void CashPaymentView_Loaded(object sender, RoutedEventArgs e)
        {
            txtScanBarcode.Focus();
        }

        private void txtScanBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string barcodeId = txtScanBarcode.Text;

                paymentViewModel.SetPayUser(barcodeId);

                if (paymentViewModel.OrderUser == null)
                {
                    tbBarcodeDecodedStatus.Text = "존재하지 않는 회원";
                }
                else
                {
                    tbBarcodeDecodedStatus.Text = "";
                }

                txtScanBarcode.Text = "";
            }
        }

        private void btnResultPaymentPage_Click(object sender, RoutedEventArgs e)
        {
            if (paymentViewModel.OrderUser == null)
            {
                MessageBox.Show("바코드를 인식해주세요");
                txtScanBarcode.Focus();
                return;
            }

            paymentViewModel.PayByCash();
            NavigationService.Navigate(new Uri("/View/ResultPaymentView.xaml", UriKind.Relative));
        }
    }
}

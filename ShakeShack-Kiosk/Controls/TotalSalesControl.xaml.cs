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

namespace ShakeShack_Kiosk.Controls
{
    /// <summary>
    /// TotalSalesControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TotalSalesControl : UserControl
    {
        private OrderHistoryViewModel orderHistoryViewModel = OrderHistoryViewModel.Instance;
        public TotalSalesControl()
        {
            InitializeComponent();
            orderHistoryViewModel.LoadOrderHistory();
        }

        private void btnCashSales_Click(object sender, RoutedEventArgs e)
        {
            orderHistoryViewModel.SetCashCashPayment();
            UpdateSalesUI();
        }

        private void btnCardSales_Click(object sender, RoutedEventArgs e)
        {
            orderHistoryViewModel.SetCardPaymentSales();
            UpdateSalesUI();
        }
        private void btnAllSales_Click(object sender, RoutedEventArgs e)
        {
            orderHistoryViewModel.SetAllPayemntSales();
            UpdateSalesUI();
        }

        private void UpdateSalesUI()
        {
            this.tbTotalSales.Text = string.Format($"총 매출: {orderHistoryViewModel.TotalSales}원");
            this.tbTotalSalesExcludeDiscount.Text = string.Format($"순수 매출: {orderHistoryViewModel.TotalSalesExcludeDiscount}원");
            this.tbDiscountPrice.Text = string.Format($"할인 금액: {orderHistoryViewModel.DiscountPrice}원");
        }
    }
}

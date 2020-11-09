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
using System.Windows.Threading;

namespace ShakeShack_Kiosk.View
{
    /// <summary>
    /// ResultPaymentView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ResultPaymentView : Page
    {
        public ResultPaymentView()
        {
            InitializeComponent();
            Loaded += ResultPaymentView_Loaded;
        }

        private void ResultPaymentView_Loaded(object sender, RoutedEventArgs e)
        {
            OrderFoodViewModel orderFoodViewModel = OrderFoodViewModel.Instance;
            PaymentViewModel paymentViewModel = PaymentViewModel.Instance;
            TableViewModel tableViewModel = TableViewModel.Instance;

            tbTotalPrice.Text = string.Format("총 금액 : {0}", orderFoodViewModel.OrderFoodTotalPrice);
            tbOrderId.Text = string.Format("주문번호 : {0}", paymentViewModel.OrderId);

            orderFoodViewModel.InitInstance();
            paymentViewModel.InitInstance();
            tableViewModel.InitInstance();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler((object _sender, EventArgs _e) =>
            {
                NavigationService.Navigate(new Uri("View/HomeView.xaml", UriKind.Relative));
                timer.Stop();
            });

            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }
    }
}

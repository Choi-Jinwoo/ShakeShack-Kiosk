using ShakeShack_Kiosk.Connection;
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

namespace ShakeShack_Kiosk.Controls
{
    /// <summary>
    /// TitleControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TitleControl : UserControl
    {
        private OrderFoodViewModel orderFoodViewModel = OrderFoodViewModel.Instance;
        private TableViewModel tableViewModel = TableViewModel.Instance;

        public event EventHandler OnNavigateToHome;


        public TitleControl()
        {
            InitializeComponent();
            tbClock.Text = DateTime.Now.ToString();
            this.DataContext = SocketConnection.Instance;
            Loaded += TitleControl_Loaded;
        }

        private void TitleControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetClockEvent();
        }

        private void SetClockEvent()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler((object sender, EventArgs e) => {
                tbClock.Text = DateTime.Now.ToString();
            });
            timer.Start();
        }

        private void imgHome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show( "주문 내용이 취소됩니다", "ShakeShack", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                orderFoodViewModel.InitInstance();
                tableViewModel.InitInstance();

                if (OnNavigateToHome != null)
                {
                    OnNavigateToHome(this, null);
                }
            }

        }
    }
}

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
using System.IO;

namespace ShakeShack_Kiosk.View
{
    /// <summary>
    /// AdminView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AdminView : Page
    {
        public AdminView()
        {
            InitializeComponent();
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += runTime;

            Timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/OrderView.xaml", UriKind.Relative));
        }

        private void runTime(object sender, EventArgs e)
        {
            int time = App.time;
            TimeSpan runTime = TimeSpan.FromSeconds(time);
            runtime.Text = runTime.ToString("hh':'mm':'ss"); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
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
    /// ManagementView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ManagementView : Page
    {
        public ManagementView()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();

            cbAutoLogin.IsChecked = App.AutoLogin;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int time = App.TotalTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            tbRunTimer.Text = timeSpan.ToString();
        }

        private void btnStat_Click(object sender, RoutedEventArgs e)
        {
            ucStat.Visibility = Visibility.Visible;
            ucMember.Visibility = Visibility.Hidden;
            ucDiscount.Visibility = Visibility.Hidden;
        }

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            ucMember.Visibility = Visibility.Visible;
            ucStat.Visibility = Visibility.Hidden;
            ucDiscount.Visibility = Visibility.Hidden;
        }

        private void btnDiscount_Click(object sender, RoutedEventArgs e)
        {
            ucDiscount.Visibility = Visibility.Visible;
            ucMember.Visibility = Visibility.Hidden;
            ucStat.Visibility = Visibility.Hidden;
        }

        private void cbAutoLogin_Checked(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(App.autoSavePath, "1");
        }

        private void cbAutoLogin_Unchecked(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(App.autoSavePath, "0");
        }
    }
}

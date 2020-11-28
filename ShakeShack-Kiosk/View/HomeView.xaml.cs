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
    /// HomeView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HomeView : Page
    {
        public HomeView()
        {
            InitializeComponent();
            mdAdPlayer.Play();
            Loaded += HomeView_Loaded;
        }

        private void HomeView_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.KeyDown += Window_KeyDown;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (NavigationService != null)
            {
                if (NavigationService.CurrentSource == new Uri("View/HomeView.xaml", UriKind.Relative)) 
                {
                    if (e.Key == Key.F2)
                    {
                        NavigationService.Navigate(new Uri("/View/ManagementView.xaml", UriKind.Relative));
                    }
                }
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/OrderView.xaml", UriKind.Relative));
        }

        private void myMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            mdAdPlayer.Stop();
            mdAdPlayer.Position = TimeSpan.FromSeconds(0);
            mdAdPlayer.Play();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/ManagementView.xaml", UriKind.Relative));
        }
    }
}

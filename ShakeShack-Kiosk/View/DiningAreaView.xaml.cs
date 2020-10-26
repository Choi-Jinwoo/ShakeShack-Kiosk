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
    /// DiningAreaView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DiningAreaView : Page
    {
        public DiningAreaView()
        {
            InitializeComponent();
        }

        private void btnPrePage_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void btnToGo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnForHere_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("View/TableView.xaml", UriKind.Relative));
        }
    }
}

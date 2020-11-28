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
    /// LoginView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();

            Loaded += LoginView_Loaded;
        }

        private void LoginView_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.AutoLogin == true)
            {
                NavigationService.Navigate(new Uri("/View/HomeView.xaml", UriKind.Relative));
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!(tbId.Text == "user" && tbPw.Text == "1234"))
            {
                MessageBox.Show("아아디, 비밀번호가 틀렸습니다.");
            } else
            {
                NavigationService.Navigate(new Uri("/View/HomeView.xaml", UriKind.Relative));
            }
        }
    }
}

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

namespace ShakeShack_Kiosk.Controls
{
    /// <summary>
    /// MemberControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MemberControl : UserControl
    {
        private UserViewModel userViewModel = new UserViewModel();
        private OrderHistoryViewModel orderHistoryviewModel = OrderHistoryViewModel.Instance;

        public MemberControl()
        {
            InitializeComponent();
            dgMember.ItemsSource = userViewModel.Users;
        }
        private void dgMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMember.SelectedIndex == -1) return;

            User user = (User)dgMember.SelectedItem;
            dgMemberOrderHistory.ItemsSource = orderHistoryviewModel.GetFoodChartItemsByUser(user.Id);
        }
    }
}

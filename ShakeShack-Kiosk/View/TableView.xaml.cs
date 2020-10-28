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
    /// TableView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TableView : Page
    {
                private TableViewModel tableViewModel = TableViewModel.Instance;
        public TableView()
        {
            InitializeComponent();
            lstTable.ItemsSource = tableViewModel.tables;
        }

        private void btnPrePage_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void btnPaymentView_Click(object sender, RoutedEventArgs e)
        {
            if (tableViewModel.SelectedTable == null)
            {
                MessageBox.Show("테이블을 선택해주세요");
                return;
            }

            NavigationService.Navigate(new Uri("/View/PaymentView.xaml", UriKind.Relative));
        }

        private void lstTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstTable.SelectedIndex == -1) return;

            DiningTable table = (DiningTable)lstTable.SelectedItem;

            if (table.IsUsing)
            {
                MessageBox.Show("사용중인 테이블입니다");
                return;
            }

            tableViewModel.SelectedTable = table;
        }
    }
}

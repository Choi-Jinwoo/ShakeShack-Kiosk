using ShakeShack_Kiosk.Database.Dao;
using ShakeShack_Kiosk.Model;
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
    /// CashPaymentView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CashPaymentView : Page
    {
        private UserDao userDao = new UserDao();

        public CashPaymentView()
        {
            InitializeComponent();
        }

        private void txtScanBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string barcodeId = txtScanBarcode.Text;

                
            }
        }
    }
}

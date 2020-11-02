using ShakeShack_Kiosk.Database.Dao;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShakeShack_Kiosk.ViewModel
{
    class PaymentViewModel
    {
        private UserDao userDao = new UserDao();

        public void PayWithBarcode(string barcodeId)
        {
            // 회원이 존재하는지 확인
            User user = userDao.GetUserByBarcodeId(barcodeId);
            if (user == null)
            {
                MessageBox.Show("바코드를 확인해주세요");
                return;
            }
        }

        public void PayWithQRCode(string qrcodeId)
        {

        }
    }
}

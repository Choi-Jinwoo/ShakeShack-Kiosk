using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.SQLMapper
{
    static class UserSQLMapper
    {
        public static string FindUserByBarcodeSQL(string barcodeId) =>
            string.Format("SELECT id, name, barcode_id, qrcode_id FROM user WHERE barcode_id = {0};", barcodeId);
        public static string FindUserByQRCodeSQL(string qrcodeId) =>
            string.Format("SELECT id, name, barcode_id, qrcode_id FROM user WHERE qrcode_id = {0};", qrcodeId);
    }
}

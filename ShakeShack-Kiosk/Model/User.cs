using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Model
{
    class User
    {
        public int Id { get; set; }
        public string BarcodeId { get; set; }
        public string QRCodeID { get; set; }
        public string Name { get; set; }
    }
}

using ShakeShack_Kiosk.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Model
{
    class OrderHistory
    {
        public int id { get; set;  }
        public string UserId { get; set; }
        public int FoodId { get; set; }
        public int? TableNumber { get; set; }
        public int Count { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

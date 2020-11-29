using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Model
{
    class Food
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
        public int DiscountedPrice { get; set; }
        public bool IsSoldOut { get; set; }
    }
}

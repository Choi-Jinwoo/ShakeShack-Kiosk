using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Model
{
    class FoodChartItem
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int Count { get; set; } = 0;
        public int Price { get; set; } = 0;
    }
}

using MySqlConnector;
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


        public static Food Map(MySqlDataReader reader)
        {
            return new Food
            {
                Id = Convert.ToInt32(reader["id"]),
                CategoryId = Convert.ToInt32(reader["category_id"]),
                Name = Convert.ToString(reader["name"]),
                ImagePath = Convert.ToString(reader["image_path"]),
                Price = Convert.ToInt32(reader["price"]),
                DiscountedPrice = Convert.ToInt32(reader["discounted_price"]),
                IsSoldOut = Convert.ToBoolean(reader["is_sold_out"]),
            };
        }
    }
}

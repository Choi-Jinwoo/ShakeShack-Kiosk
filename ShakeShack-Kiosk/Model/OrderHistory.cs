using MySqlConnector;
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
        public int OrderId { get; set;  }
        public string UserId { get; set; }
        public int FoodId { get; set; }
        public int? TableNumber { get; set; }
        public int Count { get; set; }
        public int PaymentMethod { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public static OrderHistory Map(MySqlDataReader reader)
        {
            OrderHistory orderHistory = new OrderHistory
            {
                OrderId = Convert.ToInt32(reader["order_id"]),
                UserId = (string)reader["user_id"],
                FoodId = Convert.ToInt32(reader["food_id"]),
                Count = Convert.ToInt32(reader["count"]),
                PaymentMethod = Convert.ToInt32(reader["payment_method"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                Price = Convert.ToInt32(reader["price"])
            };

            if (reader["table_number"] == DBNull.Value)
            {
                orderHistory.TableNumber = null;
            } else
            {
                orderHistory.TableNumber = Convert.ToInt32(reader["table_number"]);
            }

            return orderHistory;
        }


    }
}

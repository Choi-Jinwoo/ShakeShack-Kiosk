using MySqlConnector;
using ShakeShack_Kiosk.Database.SQLMapper;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.Dao
{
    class OrderHistoryDao
    {
        public void CreateOrderHistory(int orderId, OrderHistory orderHistory)
        {
            DBConnection connection = new DBConnection();
            connection.Connect();
            connection.SetCommand(OrderHistorySQLMapper.InsertOrderHistorySQL(orderId, orderHistory));
            connection.Execute();
            connection.CloseConnection();
        }

        public int GetLastOrderId()
        {
            DBConnection connection = new DBConnection();
            connection.Connect();
            connection.SetCommand(OrderHistorySQLMapper.GetLastOrderIdSQL);
            MySqlDataReader reader =  connection.ExecuteQuery();

            if (reader.Read() && reader["order_id"] != System.DBNull.Value)
            {
                return Convert.ToInt32(reader["order_id"]);
            }

            return 1;
        }

        public List<OrderHistory> GetOrderHistories()
        {
            DBConnection connection = new DBConnection();
            connection.Connect();
            connection.SetCommand(OrderHistorySQLMapper.GetOrderHistoriesSQL);
            MySqlDataReader reader = connection.ExecuteQuery();

            List<OrderHistory> orderHistories = new List<OrderHistory>();
            while (reader.Read())
            {
                OrderHistory orderHistory = new OrderHistory()
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

                orderHistories.Add(orderHistory);
            }

            connection.CloseConnection();
            return orderHistories;
         }
    }
}

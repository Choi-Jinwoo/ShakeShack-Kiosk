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
    class OrderHistoryDao : IOrderHistoryDao
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

            // 값이 있고 order_id가 null이 아닐경우
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
                OrderHistory orderHistory = OrderHistory.Map(reader);
                orderHistories.Add(orderHistory);
            }

            connection.CloseConnection();
            return orderHistories;
         }

        public List<OrderHistory> GetOrderHistoriesByDate(DateTime date)
        {
            DBConnection connection = new DBConnection();
            connection.Connect();
            connection.SetCommand(OrderHistorySQLMapper.GetOrderHistoryByDate(date.ToString("yyyy-MM-dd")));
            MySqlDataReader reader = connection.ExecuteQuery();

            List<OrderHistory> orderHistories = new List<OrderHistory>();
            while (reader.Read())
            {
                OrderHistory orderHistory = OrderHistory.Map(reader);
                orderHistories.Add(orderHistory);
            }

            connection.CloseConnection();
            return orderHistories;
        }
    }
}

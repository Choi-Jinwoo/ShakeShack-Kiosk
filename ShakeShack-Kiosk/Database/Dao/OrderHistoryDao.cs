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
    }
}

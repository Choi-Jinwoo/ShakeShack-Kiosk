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
        public void CreateOrderHistory(OrderHistory orderHistory)
        {
            DBConnection connection = new DBConnection();
            connection.Connect();
            connection.SetCommand(OrderHistorySQLMapper.InsertOrderHistorySQL(orderHistory));
            connection.Execute();
            connection.CloseConnection();
        }
    }
}

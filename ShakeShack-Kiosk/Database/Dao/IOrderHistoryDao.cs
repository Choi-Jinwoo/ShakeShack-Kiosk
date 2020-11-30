using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.Dao
{
    interface IOrderHistoryDao
    {
        public void CreateOrderHistory(int orderId, OrderHistory orderHistory);
        public int GetLastOrderId();
        public List<OrderHistory> GetOrderHistories();
        public List<OrderHistory> GetOrderHistoriesByDate(DateTime date);
    }
}

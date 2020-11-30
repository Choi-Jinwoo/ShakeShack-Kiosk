using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace ShakeShack_Kiosk.Database.Dao
{
    static class Constants
    {
        public const string EXCEL_PATH = @"c:\order_history.xls";
    }
    class OrderHistoryFileDao : IOrderHistoryDao
    {
        public void CreateOrderHistory(int orderId, OrderHistory orderHistory)
        {
        }

        public int GetLastOrderId()
        {
            throw new NotImplementedException();
        }

        public List<OrderHistory> GetOrderHistories()
        {
            throw new NotImplementedException();
        }

        public List<OrderHistory> GetOrderHistoriesByDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}

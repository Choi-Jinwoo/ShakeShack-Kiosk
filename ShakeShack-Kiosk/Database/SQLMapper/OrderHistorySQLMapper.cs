using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.SQLMapper
{
    static class OrderHistorySQLMapper
    {
        public static string InsertOrderHistorySQL(int orderId, OrderHistory orderHistory)
        {
            if (orderHistory.TableNumber == null)
            {
                return string.Format("INSERT INTO order_history (order_id, user_id, food_id, table_number, created_at, count, payment_method, price) VALUES ({0}, '{1}', {2}, {3}, '{4}', {5}, {6}, {7});",
                orderId, orderHistory.UserId, orderHistory.FoodId, "null", orderHistory.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"), orderHistory.Count, orderHistory.PaymentMethod, orderHistory.Price);
            }
            
            return string.Format("INSERT INTO order_history (order_id, user_id, food_id, table_number, created_at, count, payment_method, price) VALUES ({0}, '{1}', {2}, {3}, '{4}', {5}, {6}, {7});",
                orderId, orderHistory.UserId, orderHistory.FoodId, orderHistory.TableNumber, orderHistory.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"), orderHistory.Count, orderHistory.PaymentMethod, orderHistory.Price);
        }

        public static string GetLastOrderIdSQL => "SELECT MAX(order_id) as order_id FROM order_history";
        public static string GetOrderHistoriesSQL => "SELECT * FROM order_history";
        public static string GetOrderHistoryByDate(string date) => $"SELECT * FROM order_history WHERE DATE(created_at) = DATE('{date}')";
    }
}

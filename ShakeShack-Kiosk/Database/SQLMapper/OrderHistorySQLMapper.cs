﻿using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.SQLMapper
{
    static class OrderHistorySQLMapper
    {
        public static string InsertOrderHistorySQL(OrderHistory orderHistory)
        {
            if (orderHistory.TableNumber == null)
            {
                return string.Format("INSERT INTO order_history (user_id, food_id, table_number, created_at, count, payment_method) VALUES ('{0}', {1}, {2}, '{3}', {4}, {5});",
                orderHistory.UserId, orderHistory.FoodId, "null", orderHistory.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"), orderHistory.Count, orderHistory.PaymentMethod);
            }
            
            return string.Format("INSERT INTO order_history (user_id, food_id, table_number, created_at, count, payment_method) VALUES ('{0}', {1}, {2}, '{3}', {4}, {5});",
                orderHistory.UserId, orderHistory.FoodId, orderHistory.TableNumber, orderHistory.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"), orderHistory.Count, orderHistory.PaymentMethod);
        }
            
    }
}

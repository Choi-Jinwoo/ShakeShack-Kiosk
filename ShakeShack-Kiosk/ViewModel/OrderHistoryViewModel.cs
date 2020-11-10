using ShakeShack_Kiosk.Database.Dao;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.ViewModel
{
    class OrderHistoryViewModel
    {
        OrderHistoryDao orderHistoryDao = new OrderHistoryDao();
        FoodDao foodDao = new FoodDao();

        public int TotalSales;
        public int DiscountPrice;
        public int TotalSalesExcludeDiscount;

        // TODO: 검증
        public OrderHistoryViewModel()
        {
            List<OrderHistory> orderHistories = orderHistoryDao.GetOrderHistories();

            int totalSales = 0;
            int totalSalesExcludeDiscount= 0;
            foreach (OrderHistory orderHistory in orderHistories)
            {
                Food food = foodDao.GetFood(orderHistory.FoodId);
                totalSales += food.Price * orderHistory.Count;
                totalSalesExcludeDiscount += orderHistory.Count * orderHistory.Price;
            }

            this.TotalSales = totalSales;
            this.TotalSalesExcludeDiscount = totalSalesExcludeDiscount;
            this.DiscountPrice = totalSales - totalSalesExcludeDiscount;
        }
    }
}

using ShakeShack_Kiosk.Database.Dao;
using ShakeShack_Kiosk.Enum;
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
        private static OrderHistoryViewModel instance;

        private OrderHistoryViewModel()
        {
            LoadOrderHistory();
        }

        public void LoadOrderHistory()
        {
            originOrderHistories = orderHistoryDao.GetOrderHistories();
            orderHistories = originOrderHistories;
        }

        public static OrderHistoryViewModel Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderHistoryViewModel();
                }
                return instance;
            }
        }

        OrderHistoryDao orderHistoryDao = new OrderHistoryDao();
        FoodDao foodDao = new FoodDao();

        private List<OrderHistory> orderHistories = new List<OrderHistory>();
        private List<OrderHistory> originOrderHistories = new List<OrderHistory>();

        public int TotalSales;
        public int DiscountPrice;
        public int TotalSalesExcludeDiscount;

        public void SetAllPayemntSales()
        {
            orderHistories = originOrderHistories;
            this.CalcSales();
        }

        public void SetCardPaymentSales()
        {
            orderHistories = originOrderHistories
                .Where(orderHistory => orderHistory.PaymentMethod == (int)PaymentMethodEnum.CARD)
                .ToList();
            this.CalcSales();
        }

        public void SetCashCashPayment()
        {
            orderHistories = originOrderHistories
                .Where(orderHistory => orderHistory.PaymentMethod == (int) PaymentMethodEnum.CASH)
                .ToList();
            this.CalcSales();
        }

        private void CalcSales()
        {
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

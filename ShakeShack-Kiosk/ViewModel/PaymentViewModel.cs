using ShakeShack_Kiosk.Database.Dao;
using ShakeShack_Kiosk.Enum;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShakeShack_Kiosk.ViewModel
{
    class PaymentViewModel
    {
        private static PaymentViewModel instance;
        private PaymentViewModel() { }

        public static PaymentViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PaymentViewModel();
                }
                return instance;
            }
        }

        private UserDao userDao = new UserDao();
        private OrderHistoryDao orderHistoryDao = new OrderHistoryDao();
        private OrderFoodViewModel orderFoodViewModel = OrderFoodViewModel.Instance;
        private TableViewModel tableViewModel= TableViewModel.Instance;

        public User OrderUser;

    
        public void PayByCash(string userId)
        {
            Pay(userId, PaymentMethodEnum.CASH);
        }

        public void PayByCard(string userId)
        {
            Pay(userId, PaymentMethodEnum.CARD);
        }

        private void Pay(string userId, PaymentMethodEnum paymentMethod)
        {
            // 회원이 존재하는지 확인
            User user = userDao.GetUser(userId);
            if (user == null)
            {
                OrderUser = null;
                return;    
            }

            OrderUser = user;

            foreach (OrderFood orderFood in orderFoodViewModel.OrderFoods)
            {
                OrderHistory orderHistory = new OrderHistory()
                {
                    UserId = user.Id,
                    FoodId = orderFood.Food.Id,
                    Count = orderFood.Count,
                    TableNumber = tableViewModel.SelectedTable?.Number,
                    PaymentMethod = (int) paymentMethod
                };

                orderHistoryDao.CreateOrderHistory(orderHistory);
            }           

            // 결제 완료 후 인스턴스 초기화
            orderFoodViewModel.InitInstance();
            tableViewModel.InitInstance();
        }

        public void InitInstance()
        {
            this.OrderUser = null;
        }
    }
}

using ShakeShack_Kiosk.Database.Dao;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.ViewModel
{
    class UserViewModel
    {
        private UserDao userDao = new UserDao();
        private OrderHistoryViewModel orderHistoryViewModel = OrderHistoryViewModel.Instance;

        public List<User> Users = new List<User>();

        public UserViewModel()
        {
            Users = userDao.GetUsers();
            foreach (User user in Users)
            {
                List<OrderHistory> orderHistories = orderHistoryViewModel.GetOrderHistoriesByUser(user.Id);
                int totalSales = 0;
                foreach (OrderHistory orderHistory in orderHistories)
                {
                    totalSales += (orderHistory.Count * orderHistory.Price);
                }

                user.TotalSales = totalSales;
            }
        }
    }
}

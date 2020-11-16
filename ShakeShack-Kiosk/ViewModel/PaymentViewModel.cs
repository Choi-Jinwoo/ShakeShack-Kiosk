using Newtonsoft.Json;
using ShakeShack_Kiosk.Connection;
using ShakeShack_Kiosk.Database.Dao;
using ShakeShack_Kiosk.Enum;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShakeShack_Kiosk.ViewModel
{
    class PaymentViewModel : INotifyPropertyChanged
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

        private User orderUser { get; set; }
        public User OrderUser
        {
            get => orderUser;
            set
            {
                orderUser = value;
                OnPropertyChanged("OrderUser");
            }
        } 

        public int OrderId { get; set; }
    
        public void SetPayUser(string userId)
        {
            // 회원이 존재하는지 확인
            User user = userDao.GetUser(userId);
            if (user == null)
            {
                OrderUser = null;
                return;    
            }

            OrderUser = user;
        }

        public void PayByCash()
        {
            Pay(PaymentMethodEnum.CASH);
        }

        // TODO: 매개변수 삭제
        public void PayByCard()
        {
            Pay(PaymentMethodEnum.CARD);
        }

        private void Pay(PaymentMethodEnum paymentMethod)
        {

            int orderId = orderHistoryDao.GetLastOrderId() + 1;
            this.OrderId = orderId;

            List<MsgPacketMenu> menus = new List<MsgPacketMenu>();

            foreach (OrderFood orderFood in orderFoodViewModel.OrderFoods)
            {
                OrderHistory orderHistory = new OrderHistory()
                {
                    UserId = OrderUser.Id,
                    FoodId = orderFood.Food.Id,
                    Count = orderFood.Count,
                    TableNumber = tableViewModel.SelectedTable?.Number,
                    PaymentMethod = (int) paymentMethod,
                    Price = orderFood.Food.DiscountedPrice,
                };

                orderHistoryDao.CreateOrderHistory(orderId, orderHistory);
                if (tableViewModel.SelectedTable != null)
                {
                    tableViewModel.SelectedTable.PaidAt = DateTime.Now;
                }

                menus.Add(new MsgPacketMenu()
                {
                    Name = orderFood.Food.Name,
                    Price = orderFood.Food.DiscountedPrice,
                    Count = orderFood.Count,
                });
            }           

            Task orderTask = new Task(() =>
            {
                try
                {
                    SocketConnection con = SocketConnection.Instance;
                    con.Connect();

                    MsgPacket packet = new MsgPacket()
                    {
                        MSGType = (int)MSGTypeEnum.ORDER_INFO,
                        Id = "2119",
                        Menus = menus,
                        OrderNumber = orderId > 100 ? (orderId % 100).ToString("000") : orderId.ToString("000"),
                    };

                    string strJson = JsonConvert.SerializeObject(packet);
                    con.Sock.Send(Encoding.UTF8.GetBytes(strJson));
                } catch (Exception e)
                { }
            });

            orderTask.Start();
        }

        public void InitInstance()
        {
            this.OrderUser = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.ViewModel
{
    // 싱글톤 클래스
    class OrderFoodViewModel : INotifyPropertyChanged
    {
        private static OrderFoodViewModel instance;
        private OrderFoodViewModel() { }

        public static OrderFoodViewModel Instance {
            get {
                if (instance == null) {
                    instance = new OrderFoodViewModel();
                }

                return instance;
            }
        }

        public ObservableCollection<OrderFood> OrderFoods { get; private set; } = new ObservableCollection<OrderFood>();

        private int orderFoodTotalPrice { get; set; } = 0;
        public int OrderFoodTotalPrice {
            get => orderFoodTotalPrice;
            
            private set {
                orderFoodTotalPrice = value;
                OnPropertyChanged("OrderFoodTotalPrice");
            }
        }

        public int OrderFoodSize => OrderFoods.Count;

        private int CalcOrderFoodTotalPrice()
        {
            int totalPrice = 0;
            foreach (OrderFood orderFood in OrderFoods)
            {
                totalPrice += orderFood.TotalPrice;
            }

            return totalPrice;
        }
        private OrderFood GetExistFood(Food food)
        {
            foreach (OrderFood orderFood in OrderFoods)
            {
                if (orderFood.Food.Equals(food))
                {
                    return orderFood;
                }
            }

            return null;
        }

        public void IncreaseOrderFood(OrderFood orderFood)
        {
            orderFood.Count += 1;
            OrderFoodTotalPrice = CalcOrderFoodTotalPrice();
        }

        public void DecreaseOrderFood(OrderFood orderFood)
        {
            if (orderFood.Count < 1)
            {
                OrderFoods.Remove(orderFood);
            } else
            {
                orderFood.Count -= 1;
            }
            OrderFoodTotalPrice = CalcOrderFoodTotalPrice();
        }

        public void AddOrderFood(Food food)
        {
            if (food.IsSoldOut == true) return;

            OrderFood existOrderFood = GetExistFood(food);

            // 해당 음식 존재하지 않는다면 리스트에 추가
            if (existOrderFood == null)
            {
                OrderFoods.Add(new OrderFood(food));
            } else
            {
                existOrderFood.Count += 1;
            }
            OrderFoodTotalPrice = CalcOrderFoodTotalPrice();
        }

        public void RemoveOrderFood(OrderFood orderFood)
        {
            OrderFoods.Remove(orderFood);
            OrderFoodTotalPrice = CalcOrderFoodTotalPrice();
        }

        public void RemoveAllOrderFood()
        {
            OrderFoods.Clear();
            OrderFoodTotalPrice = CalcOrderFoodTotalPrice();
        }

        public void InitInstance()
        {
            this.OrderFoods = new ObservableCollection<OrderFood>();
            this.OrderFoodTotalPrice = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Model
{
    class OrderFood : INotifyPropertyChanged
    {
        public Food Food { get; set; }
        private int count = 1;
        public int Count {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged("Count");
                TotalPrice = Food.Price * Count;
            }
        }
        private int totalPrice = 0;
        public int TotalPrice {
            get => totalPrice;
            set
            {
                totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public OrderFood(Food food)
        {
            this.Food = food;
            TotalPrice = Food.Price * Count;
        }
    }
}

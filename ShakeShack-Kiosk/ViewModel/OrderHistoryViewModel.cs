using ShakeShack_Kiosk.Database.Dao;
using ShakeShack_Kiosk.Enum;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.ViewModel
{
    class OrderHistoryViewModel : INotifyPropertyChanged
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
            CalcSales();
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
        CategoryDao categoryDao = new CategoryDao();

        private List<OrderHistory> orderHistories = new List<OrderHistory>();
        private List<OrderHistory> originOrderHistories = new List<OrderHistory>();

        private int totalSales;
        public int TotalSales
        {
            get => totalSales;
            set
            {
                totalSales = value;
                OnPropertyChanged("TotalSales");
            }
        }

        private int discountPrice;
        public int DiscountPrice 
        {
            get => discountPrice;
            set
            {
                discountPrice = value;
                OnPropertyChanged("DiscountPrice");
            }
        }

        private int totalSalesExcludeDiscount;
        public int TotalSalesExcludeDiscount 
        {
            get => totalSalesExcludeDiscount;
            set
            {
                totalSalesExcludeDiscount = value;
                OnPropertyChanged("TotalSalesExcludeDiscount");
            }
        }

        public void SetAllPayemntSales()
        {
            orderHistories = originOrderHistories;
            CalcSales();
        }

        public void SetCardPaymentSales()
        {
            orderHistories = originOrderHistories
                .Where(orderHistory => orderHistory.PaymentMethod == (int)PaymentMethodEnum.CARD)
                .ToList();
            CalcSales();
        }

        public void SetCashCashPayment()
        {
            orderHistories = originOrderHistories
                .Where(orderHistory => orderHistory.PaymentMethod == (int) PaymentMethodEnum.CASH)
                .ToList();
            CalcSales();
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

        public List<ChartItem> GetFoodChartItems()
        {
            List<OrderHistory> orderHistories = orderHistoryDao.GetOrderHistories();
            return GetFoodChartItemsInList(orderHistories);
        }

        public List<OrderHistory> GetOrderHistoriesByUser(string id)
        {
            List<OrderHistory> orderHistories = orderHistoryDao.GetOrderHistories();
            return orderHistories.Where(x => x.UserId == id).ToList();
        }

        public List<ChartItem> GetFoodChartItemsByUser(string id)
        {
            List<OrderHistory> orderHistories = GetOrderHistoriesByUser(id);
            return GetFoodChartItemsInList(orderHistories); 
        }

        public List<ChartItem> GetFoodChartItemByTable(int tableNubmer)
        {
            List<OrderHistory> orderHistories = orderHistoryDao.GetOrderHistories()
                .Where(x => x.TableNumber == tableNubmer)
                .ToList();
            return GetFoodChartItemsInList(orderHistories);
        }

        private List<ChartItem> GetFoodChartItemsInList(List<OrderHistory> orderHistories)
        {
            List<ChartItem> foodChartItems = new List<ChartItem>();

            foreach (OrderHistory orderHistory in orderHistories)
            {
                ChartItem foodChartItem =
                    foodChartItems.Find(x => x.Id == orderHistory.FoodId);

                // 이미 존재할 경우
                if (foodChartItem != null)
                {
                    foodChartItem.Count += orderHistory.Count;
                    foodChartItem.Price += (orderHistory.Price * orderHistory.Count);
                } else
                {
                    Food food = foodDao.GetFood(orderHistory.FoodId);
                    if (food != null)
                    {
                        ChartItem newFoodCharItem = new ChartItem()
                        {
                            Id = food.Id,
                            Name = food.Name,
                            Count = orderHistory.Count,
                            Price = (orderHistory.Price * orderHistory.Count),
                        };

                        foodChartItems.Add(newFoodCharItem);
                    }
                }
            }

            return foodChartItems;
        }

        private List<ChartItem> GetCategoryChartItemsInList(List<OrderHistory> orderHistories)
        {
            List<ChartItem> categoryChartItems = new List<ChartItem>();
            List<Category> categories = categoryDao.GetCategories();

            foreach (OrderHistory orderHistory in orderHistories)
            {
                bool isExist = false;
                foreach (ChartItem categoryChartItem in categoryChartItems)
                {
                    Food food = foodDao.GetFood(orderHistory.FoodId);
                    if (food.CategoryId == categoryChartItem.Id)
                    {
                        categoryChartItem.Price += (orderHistory.Price * orderHistory.Count);
                        categoryChartItem.Count += orderHistory.Count;
                        isExist = true;
                        break;
                    }
                }

                if (isExist == false)
                {
                    Food food = foodDao.GetFood(orderHistory.FoodId);
                    Category category = categories.Find(x => x.Id == food.CategoryId);

                    if (food != null)
                    {
                        ChartItem newCategoryChartItem = new ChartItem()
                        {
                            Id = food.CategoryId,
                            Name = category.Name,
                            Count = orderHistory.Count,
                            Price = (orderHistory.Price * orderHistory.Count),
                        };

                        categoryChartItems.Add(newCategoryChartItem);
                    }
                }
            }

            return categoryChartItems;
        }

        public List<ChartItem> GetCategoryChartItems()
        {
            List<OrderHistory> orderHistories = orderHistoryDao.GetOrderHistories();
            return GetCategoryChartItemsInList(orderHistories);
        }
        public List<ChartItem> GetCategoryChartItemsByTable(int tableNumber)
        {
            List<OrderHistory> orderHistories = orderHistoryDao.GetOrderHistories()
                .Where(x => x.TableNumber == tableNumber)
                .ToList();

            return GetCategoryChartItemsInList(orderHistories);
        }

        public int GetTodayTotalSales()
        {
            List<OrderHistory> orderHistories = orderHistoryDao.GetOrderHistoryByDate(DateTime.Now);
            int totalSales = 0;

            foreach (OrderHistory orderHistory in orderHistories)
            {
                totalSales += (orderHistory.Count * orderHistory.Price);
            }

            return totalSales;
        }

        public List<SalesByTime> GetChartItemsByTime()
        {
            List<SalesByTime> salesByTimes = new List<SalesByTime>();
            for (int i = 0; i < 24; i += 1)
            {
                salesByTimes.Add(
                    new SalesByTime()
                    {
                        TimeIdx = i,
                        Name = $"{i} ~ {i + 1}",
                    });
            }

            List<OrderHistory> orderHistories = orderHistoryDao.GetOrderHistories();
            foreach (OrderHistory orderHistory in orderHistories)
            {
                int totalPrice = orderHistory.Count * orderHistory.Price;
                int hour = Convert.ToDateTime(orderHistory.CreatedAt).Hour;

                salesByTimes[hour].Sales += totalPrice;
            }

            return salesByTimes;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}

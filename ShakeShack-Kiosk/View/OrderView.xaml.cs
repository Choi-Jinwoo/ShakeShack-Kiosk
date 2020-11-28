using ShakeShack_Kiosk.Model;
using ShakeShack_Kiosk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShakeShack_Kiosk.View
{
    static class Constants
    {
        public const int MAX_FOOD_ITEM_COUNT = 9;
    }
    /// <summary>
    /// OrderView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderView : Page
    {
        private OrderViewModel orderViewModel = new OrderViewModel();
        private OrderFoodViewModel orderFoodViewModel = OrderFoodViewModel.Instance;
        private TableViewModel tableViewModel = TableViewModel.Instance;


        public OrderView()
        {
            InitializeComponent();

            // 가장 처음 카테고리로 리스트박스 초기화
            if (orderViewModel.Categories.Count > 0)
            {
                lstFood.ItemsSource = orderViewModel.GetSelectedCategoryFoods(Constants.MAX_FOOD_ITEM_COUNT).ToList();
            }
            lstCategory.ItemsSource = orderViewModel.Categories;
            dgOrderFood.ItemsSource = orderFoodViewModel.OrderFoods;
            tbTotalPrice.DataContext = orderFoodViewModel;
        }

        private void lstCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCategory.SelectedIndex == -1) return;

            Category category = (Category)lstCategory.SelectedItem;

            orderViewModel.PageCount = 0;
            orderViewModel.SelectedCategory = category;
            lstFood.ItemsSource = orderViewModel.GetSelectedCategoryFoods(Constants.MAX_FOOD_ITEM_COUNT).ToList();
        }
        private void btnPreItem_Click(object sender, RoutedEventArgs e)
        {
            int pageCount = orderViewModel.PageCount;

            if (pageCount > 0)
            {
                lstFood.ItemsSource = orderViewModel.GetSelectedCategoryFoods(
                    (pageCount - 1) * Constants.MAX_FOOD_ITEM_COUNT,
                    Constants.MAX_FOOD_ITEM_COUNT).ToList();
                orderViewModel.PageCount -= 1;
            }
        }
        private void btnNextItem_Click(object sender, RoutedEventArgs e)
        {
            int pageCount = orderViewModel.PageCount;

            List<Food> categoryFoods = orderViewModel.GetSelectedCategoryFoods();

            if (categoryFoods.Count > Constants.MAX_FOOD_ITEM_COUNT * (pageCount + 1))
            {
                lstFood.ItemsSource = orderViewModel.GetSelectedCategoryFoods(
                    (pageCount + 1) * Constants.MAX_FOOD_ITEM_COUNT,
                    Constants.MAX_FOOD_ITEM_COUNT).ToList();
                orderViewModel.PageCount += 1;
            }
        }
        private void lstFood_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFood.SelectedIndex == -1) return;

            Food food = (Food)lstFood.SelectedItem;

            // 주문 음식에 추가
            orderFoodViewModel.AddOrderFood(food);
            lstFood.SelectedIndex = -1;
        }
        private void btnIncreaseOrderFood_Click(object sender, RoutedEventArgs e)
        {
            Button btnIncraseOrderFood = (Button)sender;
            OrderFood orderFood = (OrderFood)btnIncraseOrderFood.DataContext;

            orderFoodViewModel.IncreaseOrderFood(orderFood);
        }

        private void btnDecreaseOrderFood_Click(object sender, RoutedEventArgs e)
        {
            Button btnDecraseOrderFood = (Button)sender;
            OrderFood orderFood = (OrderFood)btnDecraseOrderFood.DataContext;

            orderFoodViewModel.DecreaseOrderFood(orderFood);
        }
        private void btnDeleteOrderFood_Click(object sender, RoutedEventArgs e)
        {
            Button btnDeleteOrderFood = (Button)sender;
            OrderFood orderFood = (OrderFood)btnDeleteOrderFood.DataContext;

            orderFoodViewModel.RemoveOrderFood(orderFood);
        }
        private void btnDeleteAllOrderFood_Click(object sender, RoutedEventArgs e)
        {
            // 주문음식이 존재할 경우
            if (orderFoodViewModel.OrderFoodSize > 0)
            {
                if (MessageBox.Show( "주문 내용이 취소됩니다", "ShakeShack", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    orderFoodViewModel.RemoveAllOrderFood();
                }
            }
        }
        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (orderFoodViewModel.OrderFoodSize > 0)
            {
                NavigationService.Navigate(new Uri("/View/DiningAreaView.xaml", UriKind.Relative));
            }
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (orderFoodViewModel.OrderFoodSize > 0)
            {
                if (MessageBox.Show( "주문 내용이 취소됩니다", "ShakeShack", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    orderFoodViewModel.InitInstance();
                    tableViewModel.InitInstance();
                    NavigationService.Navigate(new Uri("/View/HomeView.xaml", UriKind.Relative));
                }
            } else
            {
                NavigationService.Navigate(new Uri("/View/HomeView.xaml", UriKind.Relative));
            }
        }
    }
}

using ShakeShack_Kiosk.Database.Dao;
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

namespace ShakeShack_Kiosk.Controls
{
    /// <summary>
    /// DiscountControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DiscountControl : UserControl
    {
        List<Food> foods = new List<Food>();
        List<Food> originFoods;

        private FoodDao foodDao = new FoodDao();

        public DiscountControl()
        {
            InitializeComponent();
            foods = foodDao.GetFoods();
            dgMenu.ItemsSource = foods;
        }

        private void dgMenu_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Row.Item != null)
            {
                Food food = (Food)e.Row.Item;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<Food> foods = (List<Food>)dgMenu.ItemsSource;

            for (int i = 0; i < foods.Count; i += 1)
            {
                foodDao.UpdateFood(foods[i].Id, foods[i]);
            }

            MessageBox.Show("저장 성공");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            foods = foodDao.GetFoods();
            dgMenu.ItemsSource = foods;
        }
    }
}

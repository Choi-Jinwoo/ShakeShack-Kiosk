using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.SQLMapper
{
    static class FoodSQLMapper
    {
        public static string FindAllSQL = "SELECT id, category_id, name, image_path, price, discounted_price, is_sold_out FROM food;";
        public static string FindAllByCategorySQL(int categoryId) =>
            string.Format("SELECT id, category_id, name, image_path, price, discounted_price, is_sold_out FROM food WHERE category_id = {0};", categoryId);

        public static string FindByIdSQL(int id)
        {
            return string.Format("SELECT * FROM food WHERE id = {0};", id);
        }

        public static string UpdateFood(int id, Food food)
        {
            return $"UPDATE food SET discounted_price = {food.DiscountedPrice}, is_sold_out = {food.IsSoldOut} WHERE id = {id}";
        }
    }
}

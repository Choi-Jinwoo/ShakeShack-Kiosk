using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.SQLMapper
{
    static class FoodSQLMapper
    {
        public static string FindAllSQL = "SELECT id, category_id, name, image_path, price FROM food;";
        public static string FindAllByCategorySQL(int categoryId) =>
            string.Format("SELECT id, category_id, name, image_path, price FROM food WHERE category_id = {0};", categoryId);
    }
}

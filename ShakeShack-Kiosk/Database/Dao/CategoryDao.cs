using MySqlConnector;
using ShakeShack_Kiosk.Database.SQLMapper;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.Dao
{
    class CategoryDao
    {
        public List<Category> GetCategories()
        {
            DBConnection connection = new DBConnection();

            connection.Connect();
            connection.SetCommand(CategorySQLMapper.FindAllSQL);

            MySqlDataReader reader = connection.ExecuteQuery();

            List<Category> categories = new List<Category>();

            while (reader.Read())
            {
                Category category = Category.Map(reader);
                categories.Add(category);
            }

            connection.CloseConnection();

            return categories;
        }
    }
}

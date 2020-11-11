using MySqlConnector;
using ShakeShack_Kiosk.Database.SQLMapper;
using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.Dao
{
    class FoodDao
    {
        public Food GetFood(int id)
        {
            DBConnection connection = new DBConnection();

            connection.Connect();
            connection.SetCommand(FoodSQLMapper.FindByIdSQL(id));

            MySqlDataReader reader = connection.ExecuteQuery();

            if (reader.Read())
            {
                Food food = new Food()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    CategoryId = Convert.ToInt32(reader["category_id"]),
                    Name = Convert.ToString(reader["name"]),
                    ImagePath = Convert.ToString(reader["image_path"]),
                    Price = Convert.ToInt32(reader["price"]),
                    DiscountedPrice = Convert.ToInt32(reader["discounted_price"])
                };

                connection.CloseConnection();

                return food;
            }

            connection.CloseConnection();
            return null;

        }

        public List<Food> GetFoods()
        {
            DBConnection connection = new DBConnection();

            connection.Connect();
            connection.SetCommand(FoodSQLMapper.FindAllSQL);

            MySqlDataReader reader = connection.ExecuteQuery();

            List<Food> foods = new List<Food>();
            while (reader.Read())
            {
                Food food = new Food()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    CategoryId = Convert.ToInt32(reader["category_id"]),
                    Name = Convert.ToString(reader["name"]),
                    ImagePath = Convert.ToString(reader["image_path"]),
                    Price = Convert.ToInt32(reader["price"]),
                    DiscountedPrice = Convert.ToInt32(reader["discounted_price"])
                };

                foods.Add(food);
            }

            connection.CloseConnection();

            return foods;
        }
    }
}

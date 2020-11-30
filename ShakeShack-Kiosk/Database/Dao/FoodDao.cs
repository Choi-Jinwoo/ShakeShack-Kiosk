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
                Food food = Food.Map(reader);
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
                Food food = Food.Map(reader);
                foods.Add(food);
            }
            connection.CloseConnection();

            return foods;
        }

        public void UpdateFood(int id, Food food)
        {
            DBConnection connection = new DBConnection();

            connection.Connect();
            connection.SetCommand(FoodSQLMapper.UpdateFood(id, food));
            connection.Execute();
            connection.CloseConnection();
        }
    }
}

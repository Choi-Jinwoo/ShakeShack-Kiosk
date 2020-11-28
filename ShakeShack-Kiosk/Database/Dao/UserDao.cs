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
    class UserDao
    {
        public User GetUser(string id)
        {
            DBConnection connection = new DBConnection();
            connection.Connect();
            connection.SetCommand(UserSQLMapper.FindUserSQL(id));

            MySqlDataReader reader = connection.ExecuteQuery();
            
            if (reader.Read() == false)
            {
                connection.CloseConnection();
                return null;
            }

            User user = new User()
            {
                Id = (string)reader["id"],
                Name = (string)reader["name"],
            };

            connection.CloseConnection();
            return user;
        }
        public List<User> GetUsers()
        {
            DBConnection connection = new DBConnection();
            connection.Connect();
            connection.SetCommand(UserSQLMapper.FindUsersSQL);

            MySqlDataReader reader = connection.ExecuteQuery();

            List<User> users = new List<User>();
            while (reader.Read())
            {
                User user = new User()
                {
                    Id = (string)reader["id"],
                    Name = (string)reader["name"],
                };
                users.Add(user);
            }

            connection.CloseConnection();
            return users;
        }
    }
}

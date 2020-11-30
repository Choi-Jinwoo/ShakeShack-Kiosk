using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Model
{
    class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TotalSales { get; set; }
        public static User Map(MySqlDataReader reader)
        {
            return new User
            {
                Id = (string)reader["id"],
                Name = (string)reader["name"],
            };
        }
    }
}

using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Model
{
    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static Category Map(MySqlDataReader reader)
        {
            return new Category
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = Convert.ToString(reader["name"]),
            };
        }
    }
}

using MySqlConnector;
using ShakeShack_Kiosk.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database
{
    class DBConnection
    {
        private MySqlConnection connection = null;
        private MySqlCommand command = null;

        public void Connect()
        {
            string strDBConnection = DBConfig.DBConnectionString;
            connection = new MySqlConnection(strDBConnection);
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void SetCommand(string strCommand)
        {
            command = new MySqlCommand(strCommand, connection);
        }

        public int Execute()
        {
            return command.ExecuteNonQuery();
        }

        public MySqlDataReader ExecuteQuery()
        {
            return command.ExecuteReader();
        }
    }
}

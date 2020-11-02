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
        public User GetUserByBarcodeId(string barcodeId)
        {
            DBConnection connection = new DBConnection();
            connection.Connect();
            connection.SetCommand(UserSQLMapper.FindUserByBarcodeSQL(barcodeId));

            MySqlDataReader reader = connection.ExecuteQuery();
            
            if (reader.Read() == false)
            {
                connection.CloseConnection();
                return null;
            }

            User user = new User()
            {
                Id = Convert.ToInt32(reader["id"]),
                BarcodeId = (string)reader["barcode_id"],
                QRCodeID = (string)reader["qrcode_id"],
                Name = (string)reader["name"],
            };

            connection.CloseConnection();
            return user;
        }

        public User GetUserByQRCodeId(string qrcodeId)
        {
            DBConnection connection = new DBConnection();
            connection.Connect();
            connection.SetCommand(UserSQLMapper.FindUserByQRCodeSQL(qrcodeId));

            MySqlDataReader reader = connection.ExecuteQuery();
            
            if (reader.Read() == false)
            {
                connection.CloseConnection();
                return null;
            }

            User user = new User()
            {
                Id = Convert.ToInt32(reader["id"]),
                BarcodeId = (string)reader["barcode_id"],
                QRCodeID = (string)reader["qrcode_id"],
                Name = (string)reader["name"],
            };

            connection.CloseConnection();
            return user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.SQLMapper
{
    static class UserSQLMapper
    {
        public static string FindUserSQL(string id) =>
            string.Format("SELECT id, name FROM user WHERE id = '{0}';", id);
    }
}

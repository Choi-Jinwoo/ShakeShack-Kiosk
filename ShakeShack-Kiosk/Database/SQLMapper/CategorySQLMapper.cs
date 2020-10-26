a
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Database.SQLMapper
{
    static class CategorySQLMapper
    {
        public static string FindAllSQL = "SELECT id, name FROM category;";
    }
}

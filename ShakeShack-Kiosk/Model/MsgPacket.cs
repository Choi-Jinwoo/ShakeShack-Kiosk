using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.Model
{
    public class MsgPacket
    {
        public int MSGType { get; set; }
        public bool Group { get; set; }
        public string Id { get; set; } = "";
        public string Content { get; set; } = "";
        public string ShopName { get; set; } = "ShakeShack Burger";
        public string OrderNumber { get; set; } = "";
        public List<MsgPacketMenu> Menus { get; set; }
    }

    public class MsgPacketMenu
    {
        public string Name { get; set; } = "";
        public int Count { get; set; }
        public int Price { get; set; }
    }
}

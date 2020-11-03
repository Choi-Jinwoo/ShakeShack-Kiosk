using ShakeShack_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakeShack_Kiosk.ViewModel
{
    class TableViewModel
    {
        private static TableViewModel instance;
        private TableViewModel() { }

        public static TableViewModel Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableViewModel();
                }
                return instance;
            }
        }

        public DiningTable SelectedTable;

        public List<DiningTable> tables = new List<DiningTable>( new DiningTable[]
        {
            new DiningTable() {Number=1},
            new DiningTable() {Number=2},
            new DiningTable() {Number=3},
            new DiningTable() {Number=4},
            new DiningTable() {Number=5},
            new DiningTable() {Number=6},
            new DiningTable() {Number=7},
            new DiningTable() {Number=8},
            new DiningTable() {Number=9},
        });

        public void InitInstance()
        {
            this.SelectedTable = null;
        }
    }
}

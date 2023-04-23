using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int MeasuringUnitId { get; set; }
        public MeasuringUnit MeasuringUnit { get; set; }

        public List<StockItem> StockItems { get; set; }

        public List<RequestItem> RequestItems { get; set; }

        public Item()
        {
            StockItems = new List<StockItem>();
            RequestItems = new List<RequestItem>();
        }
    }
}

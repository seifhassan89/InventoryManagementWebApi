using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StockItem
    {

        public int StockItemId { get; set; }

        public int StockId { get; set; }

        public Stock Stock { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public StockItem() { }

    }
}

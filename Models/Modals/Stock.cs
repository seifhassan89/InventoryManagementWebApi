using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Stock
    {


        public int StockId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public int? MangerId { get; set; }

        public User Manger { get; set; }

        public List<StockItem> StockItems { get; set; }

        public List<Request> SupplyRequests { get; set; }
        public List<Request> WithdrawRequests { get; set; }

        public Stock()
        {
            StockItems = new List<StockItem>();
            SupplyRequests = new List<Request>();
            WithdrawRequests = new List<Request>();
        }



    }
}

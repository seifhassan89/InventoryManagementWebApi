

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Request
    {
        public int RequestId { get; set; }

        public string Code { get; set; }

        public DateTime Date { get; set; }

        public int RequestTypeId { get; set; }

        public RequestType RequestType { get; set; }
        public List<RequestItem> RequestItems { get; set; }

        public int? StockFromId { get; set; }

        public Stock StockFrom { get; set; }

        public int? StockToId { get; set; }

        public Stock StockTo { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }



        public Request()
        {

            RequestItems = new List<RequestItem>();
        }

    }
}

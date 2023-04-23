using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RequestItemDTO
    {
        public int RequestItemId { get; set; }

        public int RequestId { get; set; }
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }


        public DateTime ProductionDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public RequestItemDTO() { }
    }
}

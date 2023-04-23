using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RequestType
    {

        public int RequestTypeId { get; set; }

        public string Name { get; set; }

        public List<Request> Requests { get; set; }

        public RequestType()
        {
            Requests = new List<Request>();

        }
    }
}

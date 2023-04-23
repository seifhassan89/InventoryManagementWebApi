using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public List<Request> requests { get; set; }

        public List<Stock> Stocks { get; set; }

        public User()
        {

            requests = new List<Request>();
            Stocks = new List<Stock>();

        }
    }
}
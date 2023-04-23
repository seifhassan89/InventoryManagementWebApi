using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserDTO
    {

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string? NameOrEmail { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MeasuringUnit
    {
        public int MeasuringUnitId { get; set; }
        public string Name { get; set; }

        public List<Item> Items { get; set; }

        public MeasuringUnit()
        {
            Items = new List<Item>();
        }

    }
}

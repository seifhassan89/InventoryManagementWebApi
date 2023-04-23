

namespace Models
{
    public class FilterStockDTO
    {
        public string NameOrAddress { get; set; }
        public List<int> MangerIds { get; set; }
        public FilterStockDTO()
        {

            MangerIds = new List<int>();
        }

    }
}



namespace Models
{
    public class StockDTO
    {
        public int StockId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public int? MangerId { get; set; }

        public UserDTO? Manger { get; set; }
    }
}

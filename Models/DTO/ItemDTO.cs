

namespace Models
{
    public class ItemDTO
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int MeasuringUnitId { get; set; }
        public string? MeasuringUnitName { get; set; }

        public List<StockDTO> Stocks { get; set; }

        //public List<RequestItem> RequestItems { get; set; }
        public ItemDTO()
        {
            Stocks = new List<StockDTO>();
        }

    }
}

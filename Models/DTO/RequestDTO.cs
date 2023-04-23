


namespace Models
{
    public class RequestDTO
    {
        public int RequestId { get; set; }

        public string Code { get; set; }

        public DateTime Date { get; set; }

        public int RequestTypeId { get; set; }

        public RequestTypeDTO? RequestType { get; set; }

        public List<RequestItemDTO> RequestItems { get; set; }

        public int? StockFromId { get; set; }

        public StockDTO? StockFrom { get; set; }

        public int? StockToId { get; set; }

        public StockDTO? StockTo { get; set; }

        public int UserId { get; set; }
        public UserDTO? User { get; set; }

        public RequestDTO()
        {
            RequestItems = new List<RequestItemDTO>();
        }

    }
}

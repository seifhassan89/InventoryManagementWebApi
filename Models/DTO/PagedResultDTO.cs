namespace employee_task.Models
{
    public class PagedResultDTO<T>
    {
        public List<T> List { get; set; }
        public int PageNumber { get; set; }
        public int TotalRecords { get; set; }
        public PagedResultDTO()
        {
            List = new List<T>();
        }
    }
}
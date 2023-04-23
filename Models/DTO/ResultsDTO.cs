using System.Web.Http.Results;

namespace Models
{
    public class ResultDTO<T>
    {
        public T Results { get; set; }
        public List<string> ErrorsMessages { get; set; }
        public List<string> Messages { get; set; }
        public int StatusCode { get; set; }

        public ResultDTO()
        {
            ErrorsMessages = new List<string>();
            Messages = new List<string>();
        }


    }
}
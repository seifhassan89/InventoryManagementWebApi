using employee_task.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IRequestService
    {
        public List<RequestDTO> GetAll(string? filterObjJson);
        public PagedResultDTO<RequestDTO> GetAllPaged(int currentPage, int pageSize, string? filterObjJson);
        public RequestDTO? GetById(int id);
        public RequestDTO AddRequest(RequestDTO requestDto);
        public bool EditRequest(int requestId, RequestDTO requestDTO);
        public bool DeleteRequest(int requestId);
        public void SaveChanges();
        public List<string> Validation(int? id, RequestDTO requestDTO);
    }
}

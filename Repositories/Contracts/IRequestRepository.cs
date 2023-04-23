using Data;
using Models;
using System;
using System.Linq;

namespace Repositories.Contracts
{
    public interface IRequestRepository : IBaseRepository
    {
        List<RequestDTO> GetAll(FilterRequestDTO filterObj);
        List<Request> GetAllPaged(int currentPage, int pageSize, FilterRequestDTO? filterObj);
        int GetAllCount(FilterRequestDTO filterObj);
        RequestDTO? GetById(int requestId);
        Request? GetEntityById(int requestId);
        void DeleteRequest(Request? request);
        Request AddRequest(Request request);
        bool EditRequest(int requestId, Request request);

    }
}

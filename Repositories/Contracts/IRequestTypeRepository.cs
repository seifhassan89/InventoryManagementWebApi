using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRequestTypeRepository : IBaseRepository
    {
        List<RequestType> GetAll();

        RequestType? GetById(int requestTypeId);

        bool DeleteRequestType(int requestTypeId);

        RequestType AddRequestType(RequestType requestType);

        bool EditRequestType(int requestTypeId, RequestType requestType);

    }
}

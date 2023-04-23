using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.Contracts
{
    public interface IRequestTypeMapper
    {
        RequestType MapToRequestType(RequestTypeDTO requestTypeDTO);

        RequestTypeDTO MapToRequestTypeDTO(RequestType requestType);
    }
}

using Models;
using Mappers.Contracts;

namespace Mappers
{
    public class RequestTypeMapper : IRequestTypeMapper
    {
        public RequestType MapToRequestType(RequestTypeDTO requestTypeDTO)
        {
            RequestType requestType = new RequestType();
            requestType.Name = requestTypeDTO.Name;
            requestType.RequestTypeId = requestTypeDTO.RequestTypeId;
            return requestType;
        }

        public RequestTypeDTO MapToRequestTypeDTO(RequestType requestType)
        {
            RequestTypeDTO requestTypeDTO = new RequestTypeDTO();
            requestTypeDTO.Name = requestType.Name;
            requestTypeDTO.RequestTypeId = requestType.RequestTypeId;

            return requestTypeDTO;
        }
    }
}
using Models;
using Mappers.Contracts;

namespace Mappers
{
    public class RequestMapper : IRequestMapper
    {
        public Request MapToRequest(RequestDTO requestDTO)
        {
            Request request = new Request();
            request.RequestId = requestDTO.RequestId;
            request.Code = requestDTO.Code;
            request.Date = requestDTO.Date;
            request.RequestTypeId = requestDTO.RequestTypeId;
            request.StockToId = requestDTO.StockToId;
            request.StockFromId = requestDTO.StockFromId;
            request.UserId = requestDTO.UserId;
            if (requestDTO.RequestItems.Count > 0)
            {
                requestDTO.RequestItems.ForEach(item =>
                {
                    RequestItem reqItem = new RequestItem();
                    reqItem.RequestItemId = item.RequestItemId;
                    reqItem.RequestId = item.RequestId;
                    reqItem.ItemId = item.ItemId;
                    reqItem.Quantity = item.Quantity;
                    reqItem.ExpirationDate = item.ExpirationDate;
                    reqItem.ProductionDate = item.ProductionDate;
                    request.RequestItems.Add(reqItem);
                });
            }
            return request;
        }

        public RequestDTO MapToRequestDTO(Request request)
        {
            RequestDTO requestDTO = new RequestDTO();
            requestDTO.RequestId = request.RequestId;
            requestDTO.Code = request.Code;
            requestDTO.Date = request.Date;

            requestDTO.StockToId = request.StockToId;
            if (request.RequestType != null)
            {
                RequestTypeDTO reqTypeDTO = new RequestTypeDTO();
                reqTypeDTO.Name = request.RequestType.Name;
                reqTypeDTO.RequestTypeId = request.RequestType.RequestTypeId;
                requestDTO.RequestType = reqTypeDTO;
            }

            requestDTO.StockFromId = request.StockFromId;
            if (request.StockFrom != null)
            {
                StockDTO stockFromDTO = new StockDTO();
                stockFromDTO.Name = request.StockFrom.Name;
                stockFromDTO.StockId = request.StockFrom.StockId;
                requestDTO.StockFrom = stockFromDTO;
            }

            requestDTO.StockToId = request.StockToId;
            if (request.StockTo != null)
            {
                StockDTO stockToDTO = new StockDTO();
                stockToDTO.StockId = request.StockTo.StockId;
                stockToDTO.Name = request.StockTo.Name;
                requestDTO.StockTo = stockToDTO;
            }

            requestDTO.UserId = request.UserId;
            if (request.User != null)
            {
                UserDTO userDTO = new UserDTO();
                userDTO.UserId = request.User.UserId;
                userDTO.Name = request.User.Name;
                requestDTO.User = userDTO;
            }

            return requestDTO;
        }
    }
}
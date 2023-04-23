using employee_task.Models;
using Models.Enum;
using Newtonsoft.Json;
using Models;
using Mappers.Contracts;
using Repositories.Contracts;
using Services.Contacts;

namespace Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestMapper _requestMapper;
        private readonly IRequestRepository _requestRepository;
        private readonly IRequestItemRepository _requestItemRepository;


        public RequestService(IRequestMapper requestMapper, IRequestRepository requestRepository, IRequestItemRepository requestItemRepository)
        {
            _requestMapper = requestMapper;
            _requestRepository = requestRepository;
            _requestItemRepository = requestItemRepository;
        }

        /// <summary>
        /// add request to DB
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public RequestDTO AddRequest(RequestDTO requestDTO)
        {
            Request request = _requestMapper.MapToRequest(requestDTO);
            Request AddedRequest = _requestRepository.AddRequest(request);
            RequestDTO addedRequest = _requestMapper.MapToRequestDTO(AddedRequest);
            return addedRequest;
        }
        /// <summary>
        /// delete request with its request items from database
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public bool DeleteRequest(int requestId)
        {
            Request? foundRequest = _requestRepository.GetEntityById(requestId);
            if (foundRequest == null)
            {
                return false;
            }
            foundRequest.RequestItems.ForEach(item =>
            {
                _requestItemRepository.DeleteRequestItem(item);
            });
            _requestRepository.DeleteRequest(foundRequest);

            return true;
        }

        public bool EditRequest(int requestId, RequestDTO requestDTO)
        {
            Request? foundRequest = _requestRepository.GetEntityById(requestId);
            Request request = _requestMapper.MapToRequest(requestDTO);
            List<RequestItem> oldRequestItem = foundRequest.RequestItems;
            List<RequestItem> newRequestItem = request.RequestItems;

            request.RequestItems = null;
            bool isEdited = _requestRepository.EditRequest(requestId, request);

            UpdateRequestItems(oldRequestItem, newRequestItem);


            return isEdited;
        }
        private void UpdateRequestItems(List<RequestItem> existingRequestItems, List<RequestItem> newRequestItems)
        {

            List<RequestItem> added = newRequestItems.Where(x => !existingRequestItems.Select(e => e.ItemId).Contains(x.ItemId)).ToList();
            List<RequestItem> deleted = existingRequestItems.Where(x => !newRequestItems.Select(e => e.ItemId).Contains(x.ItemId)).ToList();
            List<RequestItem> updated = newRequestItems.Where(x => existingRequestItems.Select(e => e.ItemId).Contains(x.ItemId)).ToList();

            added.ForEach(x => _requestItemRepository.AddRequestItem(x));
            deleted.ForEach(x => _requestItemRepository.DeleteRequestItem(x));
            updated.ForEach(x => _requestItemRepository.EditRequestItem(x.RequestItemId, x));
        }
        /// <summary>
        /// get requests list filtered
        /// </summary>
        /// <param name="filterObjJson"></param>
        /// <returns></returns>
        public List<RequestDTO> GetAll(string? filterObjJson)
        {

            FilterRequestDTO JsonObj = new FilterRequestDTO();

            if (filterObjJson != null)
            {
                JsonObj = JsonConvert.DeserializeObject<FilterRequestDTO>(filterObjJson);
            }

            List<RequestDTO> mappedList = _requestRepository.GetAll(JsonObj);
            return mappedList;
        }
        /// <summary>
        /// get requests list paged and filtered
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filterObjJson"></param>
        /// <returns></returns>
        public PagedResultDTO<RequestDTO> GetAllPaged(int pageNumber, int pageSize, string? filterObjJson)
        {
            PagedResultDTO<RequestDTO> pagedResult = new PagedResultDTO<RequestDTO>();

            FilterRequestDTO JsonObj = new FilterRequestDTO();

            if (!String.IsNullOrEmpty(filterObjJson))
            {
                JsonObj = JsonConvert.DeserializeObject<FilterRequestDTO>(filterObjJson);
            }

            List<RequestDTO> mappedRequestListDTO = new List<RequestDTO>();

            List<Request> requestsList = _requestRepository.GetAllPaged(pageNumber, pageSize, JsonObj);

            requestsList.ForEach(request =>
            {

                RequestDTO mappedRequest = _requestMapper.MapToRequestDTO(request);
                mappedRequestListDTO.Add(mappedRequest);
            });


            pagedResult.List = mappedRequestListDTO;

            pagedResult.TotalRecords = _requestRepository.GetAllCount(JsonObj);

            pagedResult.PageNumber = pageNumber;

            return pagedResult;
        }
        /// <summary>
        /// get request DTO by id with full details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RequestDTO? GetById(int id)
        {
            RequestDTO? request = _requestRepository.GetById(id);


            return request;

        }

        public void SaveChanges()
        {
            _requestRepository.SaveChanges();
        }
        /// <summary>
        /// check if request is valid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public List<string> Validation(int? id, RequestDTO requestDTO)
        {
            List<string> errors = new List<string>();

            if (String.IsNullOrEmpty(requestDTO.Code))
            {
                errors.Add("please add code request!");
            }

            if (requestDTO.Date == DateTime.MinValue)
            {
                errors.Add("date is req.");
            }
            else if (requestDTO.Date.Date < DateTime.UtcNow.Date)
            {
                errors.Add("date can't be in past");
            }

            if (requestDTO.RequestTypeId != 0)
            {
                if (requestDTO.RequestTypeId == (int)RequestTypeEnum.TransferRequest)
                {
                    if (requestDTO.StockFromId == null || requestDTO.StockToId == null)
                    {
                        errors.Add("transfer req should be send with from stock id and to stock id!");
                    }
                }

                if (requestDTO.RequestTypeId == (int)RequestTypeEnum.SupplyRequest)
                {
                    if (requestDTO.StockFromId == null)
                    {
                        errors.Add("can't make supply req with out mentioning from stock id");
                    }
                }

                if (requestDTO.RequestTypeId == (int)RequestTypeEnum.WithdrawRequest)
                {
                    if (requestDTO.StockToId == null)
                    {
                        errors.Add("can't make withdraw req with out mentioning to stock id");
                    }
                }
            }
            else
            {
                errors.Add("request type id should be sent !");
            }

            if (requestDTO.UserId == 0)
            {
                errors.Add("user issuer should be send with every req.");
            }
            if (requestDTO.RequestItems.Count == 0)
            {
                errors.Add("can't create request with out item submitte!");
            }
            else
            {
                bool isRequestItemsValid = requestDTO.RequestItems.All(x => IsRequestItemValid(x));

                if (!isRequestItemsValid)
                {
                    errors.Add("some items inputs are invalid !");
                }
            }

            return errors;

        }

        /// <summary>
        /// check if request item is valid
        /// </summary>
        /// <param name="requestItemDTO"></param>
        /// <returns></returns>
        private bool IsRequestItemValid(RequestItemDTO requestItemDTO)
        {
            bool isValid = true;

            if (requestItemDTO.ItemId == 0)
            {
                isValid = false;
            }
            else if (requestItemDTO.ProductionDate.Date > requestItemDTO.ExpirationDate.Date)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}

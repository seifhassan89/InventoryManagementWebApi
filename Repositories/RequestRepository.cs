using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Repositories.Contracts;

namespace Repositories
{
    public class RequestRepository : BaseRepository, IRequestRepository
    {
        private StocksDB _StoresDB;
        public RequestRepository(StocksDB StoresDB) : base(StoresDB)
        {

            _StoresDB = StoresDB;
        }

        /// <summary>
        /// add new request to Database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Request AddRequest(Request request)
        {
            EntityEntry<Request> addedRequest = _StoresDB.Requests.Add(request);
            return addedRequest.Entity;
        }

        /// <summary>
        /// delete request from DB
        /// </summary>
        /// <param name="request"></param>
        public void DeleteRequest(Request request)
        {
            _StoresDB.Requests.Remove(request);
        }

        /// <summary>
        /// get requests list filtered with full details
        /// </summary>
        /// <param name="filterObj"></param>
        /// <returns></returns>
        public List<RequestDTO> GetAll(FilterRequestDTO filterObj)
        {
            List<RequestDTO> list = (from request in _StoresDB.Requests

                                     join reqType in _StoresDB.RequestTypes
                                     on request.RequestTypeId equals reqType.RequestTypeId into reqTypeRelation
                                     from reqtype in reqTypeRelation.DefaultIfEmpty()

                                     join stock in _StoresDB.Stocks
                                     on request.StockFromId equals stock.StockId into stockFromId
                                     from stockId in stockFromId.DefaultIfEmpty()

                                     join stock in _StoresDB.Stocks
                                     on request.StockToId equals stock.StockId into stockToId
                                     from stocktoid in stockToId.DefaultIfEmpty()

                                     join user in _StoresDB.Users
                                     on request.UserId equals user.UserId into userRelation
                                     from user in userRelation.DefaultIfEmpty()

                                     select new { request, reqtype, stockId, stocktoid, user }
                        ).Select(tuple => new RequestDTO()
                        {
                            RequestId = tuple.request.RequestId,
                            Code = tuple.request.Code,
                            Date = tuple.request.Date,
                            StockToId = tuple.stocktoid.StockId,
                            StockTo = new StockDTO()
                            {
                                StockId = tuple.stocktoid.StockId,
                                Name = tuple.stocktoid.Name,
                            },
                            StockFromId = tuple.stockId.StockId,
                            StockFrom = new StockDTO()
                            {
                                StockId = tuple.stockId.StockId,
                                Name = tuple.stockId.Name,
                            },
                            RequestTypeId = tuple.reqtype.RequestTypeId,
                            RequestType = new RequestTypeDTO()
                            {
                                RequestTypeId = tuple.reqtype.RequestTypeId,
                                Name = tuple.reqtype.Name,
                            },
                            UserId = tuple.user.UserId,
                            User = new UserDTO()
                            {
                                UserId = tuple.user.UserId,
                                Name = tuple.user.Name,
                            }

                        }).ToList();

            return list;
        }

        /// <summary>
        /// get request list paginated and filtered
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="filterObj"></param>
        /// <returns></returns>
        public List<Request> GetAllPaged(int currentPage, int pageSize, FilterRequestDTO? filterObj)
        {
            List<Request> list = _StoresDB.Requests.Include(req => req.RequestType)
                .Include(req => req.StockFrom)
                .Include(req => req.StockTo)
                .Include(req => req.User)
                .Where(f =>
            (String.IsNullOrEmpty(filterObj.Code) || f.Code.Contains(filterObj.Code)) &&
            (filterObj.StockFromId == null || f.StockFromId.Equals(filterObj.StockFromId)) &&
            (filterObj.StockToId == null || f.StockToId.Equals(filterObj.StockToId)) &&
            (filterObj.RequestTypeId == null || f.RequestTypeId.Equals(filterObj.RequestTypeId))
            ).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return list;
        }

        /// <summary>
        /// get requests count
        /// </summary>
        /// <param name="filterObj"></param>
        /// <returns></returns>
        public int GetAllCount(FilterRequestDTO? filterObj)
        {
            int count = _StoresDB.Requests.Where(f =>
            (String.IsNullOrEmpty(filterObj.Code) || f.Code.Contains(filterObj.Code)) &&
            (filterObj.StockFromId == null || f.StockFromId.Equals(filterObj.StockFromId)) &&
            (filterObj.StockToId == null || f.StockToId.Equals(filterObj.StockToId)) &&
            (filterObj.RequestTypeId == null || f.RequestTypeId.Equals(filterObj.RequestTypeId))
            ).Count();

            return count;
        }

        /// <summary>
        /// get request by id with full details
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public RequestDTO? GetById(int requestId)
        {
            RequestDTO? requestDTO = (from request in _StoresDB.Requests

                                      join reqType in _StoresDB.RequestTypes
                                      on request.RequestTypeId equals reqType.RequestTypeId into reqTypeRelation
                                      from reqtype in reqTypeRelation.DefaultIfEmpty()

                                      join stock in _StoresDB.Stocks
                                      on request.StockFromId equals stock.StockId into stockFromId
                                      from stockId in stockFromId.DefaultIfEmpty()

                                      join stock in _StoresDB.Stocks
                                      on request.StockToId equals stock.StockId into stockToId
                                      from stocktoid in stockToId.DefaultIfEmpty()

                                      join user in _StoresDB.Users
                                      on request.UserId equals user.UserId into userRelation
                                      from user in userRelation.DefaultIfEmpty()

                                      join RequestItems in _StoresDB.RequestItems
                                      on request.RequestId equals RequestItems.RequestId into requestItemsRelation
                                      from requestItems in requestItemsRelation.DefaultIfEmpty()

                                      join itemstable in _StoresDB.Items
                                      on requestItems.ItemId equals itemstable.ItemId into getItems
                                      from itemsArray in getItems.DefaultIfEmpty()

                                      where request.RequestId == requestId
                                      select new { request, reqtype, stockId, stocktoid, user, itemsArray, requestItems }
                        ).GroupBy(i => new
                        {
                            RequestId = i.request.RequestId,
                            Code = i.request.Code,
                            Date = i.request.Date,
                            StockToId = i.stocktoid.StockId,
                            StockToName = i.stocktoid.Name,

                            StockFromId = i.stockId.StockId,
                            StockFromName = i.stockId.Name,

                            RequestTypeId = i.reqtype.RequestTypeId,
                            RequestTypeName = i.reqtype.Name,

                            UserId = i.user.UserId,
                            UserName = i.user.Name

                        })
                        .Select(i => new RequestDTO()
                        {
                            RequestId = i.Key.RequestId,
                            Code = i.Key.Code,
                            Date = i.Key.Date,
                            StockToId = i.Key.StockToId,
                            StockTo = new StockDTO()
                            {
                                StockId = i.Key.StockToId,
                                Name = i.Key.StockToName
                            },
                            StockFromId = i.Key.StockFromId,
                            StockFrom = new StockDTO()
                            {
                                StockId = i.Key.StockToId,
                                Name = i.Key.StockFromName
                            },
                            RequestTypeId = i.Key.StockFromId,
                            RequestType = new RequestTypeDTO()
                            {
                                RequestTypeId = i.Key.RequestTypeId,
                                Name = i.Key.RequestTypeName,
                            },
                            UserId = i.Key.UserId,
                            User = new UserDTO()
                            {
                                UserId = i.Key.UserId,
                                Name = i.Key.UserName
                            },
                            RequestItems = i.Where(c => c.itemsArray != null).Select(item => new RequestItemDTO()
                            {
                                ItemId = item.itemsArray.ItemId,
                                ItemName = item.itemsArray.Name,
                                Quantity = item.requestItems.Quantity,
                                ExpirationDate = item.requestItems.ExpirationDate,
                                ProductionDate = item.requestItems.ProductionDate,
                                RequestItemId = item.requestItems.RequestItemId,


                            }).ToList(),
                        }
                        ).FirstOrDefault();



            return requestDTO;
        }

        /// <summary>
        /// get request entity by id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public Request? GetEntityById(int requestId)
        {
            Request? req = _StoresDB.Requests.Include(r => r.RequestItems).Where(r => r.RequestId == requestId).FirstOrDefault();
            return req;
        }

        /// <summary>
        /// edit request data
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool EditRequest(int requestId, Request request)
        {
            Request? foundRequest = _StoresDB.Requests.Find(requestId);

            if (foundRequest == null)
            {
                return false;
            }
            else
            {
                _StoresDB.Entry(foundRequest).State = EntityState.Detached;
            }

            _StoresDB.Attach(request);
            _StoresDB.Entry(request).State = EntityState.Modified;

            return true;

        }
    }
}
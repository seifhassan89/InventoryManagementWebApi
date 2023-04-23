using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Repositories.Contracts;

namespace Repositories
{
    public class RequestTypeRepository : BaseRepository, IRequestTypeRepository
    {
        private StocksDB _StoresDB;
        public RequestTypeRepository(StocksDB StoresDB) : base(StoresDB)
        {

            _StoresDB = StoresDB;
        }
        /// <summary>
        /// add request type to database
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        public RequestType AddRequestType(RequestType requestType)
        {
            EntityEntry<RequestType> addedRequestType = _StoresDB.RequestTypes.Add(requestType);

            return addedRequestType.Entity;
        }
        /// <summary>
        /// delete request type from database
        /// </summary>
        /// <param name="requestTypeId"></param>
        /// <returns></returns>
        public bool DeleteRequestType(int requestTypeId)
        {
            RequestType? foundRequestType = _StoresDB.RequestTypes.Find(requestTypeId);
            if (foundRequestType != null)
            {
                _StoresDB.RequestTypes.Remove(foundRequestType);
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// get all request types
        /// </summary>
        /// <returns></returns>
        public List<RequestType> GetAll()
        {
            List<RequestType> list = _StoresDB.RequestTypes.ToList();

            return list;
        }
        /// <summary>
        /// get request type by id
        /// </summary>
        /// <param name="requestTypeId"></param>
        /// <returns></returns>
        public RequestType? GetById(int requestTypeId)
        {
            RequestType? requestType = _StoresDB.RequestTypes.FirstOrDefault(rt => rt.RequestTypeId == requestTypeId);

            if (requestType != null)
            {
                return requestType;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// edit request type data 
        /// </summary>
        /// <param name="requestTypeId"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        public bool EditRequestType(int requestTypeId, RequestType requestType)
        {
            RequestType? foundRequestType = _StoresDB.RequestTypes.Find(requestTypeId);

            if (foundRequestType != null)
            {
                _StoresDB.Entry(foundRequestType).State = EntityState.Detached;
            }
            else
            {
                return false;
            }

            _StoresDB.Attach(requestType);
            _StoresDB.Entry(requestType).State = EntityState.Modified;

            return true;

        }
    }
}
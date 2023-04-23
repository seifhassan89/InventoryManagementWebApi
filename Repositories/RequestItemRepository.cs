using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Repositories.Contracts;

namespace Repositories
{
    public class RequestItemRepository : BaseRepository, IRequestItemRepository
    {
        private StocksDB _StoresDB;
        public RequestItemRepository(StocksDB StoresDB) : base(StoresDB)
        {

            _StoresDB = StoresDB;
        }


        public bool DeleteRequestItem(RequestItem requestItem)
        {

            _StoresDB.RequestItems.Remove(requestItem);
            return true;


        }

        public RequestItem EditRequestItem(int requestItemId, RequestItem requestItem)
        {
            RequestItem? foundItem = _StoresDB.RequestItems.Find(requestItemId);

            if (foundItem != null)
            {
                _StoresDB.Entry(foundItem).State = EntityState.Detached;
            }

            _StoresDB.Attach(requestItem);
            _StoresDB.Entry(requestItem).State = EntityState.Modified;

            return requestItem;

        }

        public RequestItem AddRequestItem(RequestItem item)
        {
            EntityEntry<RequestItem> addedItem = _StoresDB.RequestItems.Add(item);

            return addedItem.Entity;
        }

    }
}
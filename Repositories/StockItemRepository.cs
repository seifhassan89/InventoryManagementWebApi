using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Repositories.Contracts;

namespace Repositories
{
    public class StockItemRepository : BaseRepository, IStockItemRepository
    {
        private StocksDB _StoresDB;
        public StockItemRepository(StocksDB StoresDB) : base(StoresDB)
        {

            _StoresDB = StoresDB;
        }


        public bool DeleteStockItem(StockItem stockItem)
        {

            _StoresDB.StockItems.Remove(stockItem);
            return true;


        }

    }
}
using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Repositories.Contracts;

namespace Repositories
{
    public class StockRepository : BaseRepository, IStockRepository
    {
        private StocksDB _StoresDB;
        public StockRepository(StocksDB StoresDB) : base(StoresDB)
        {

            _StoresDB = StoresDB;
        }
        /// <summary>
        /// add stock to database
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public Stock AddStock(Stock stock)
        {
            EntityEntry<Stock> addedStock = _StoresDB.Stocks.Add(stock);

            return addedStock.Entity;
        }

        /// <summary>
        /// delete stock from database
        /// </summary>
        /// <param name="stock"></param>
        public void DeleteStock(Stock stock)
        {
            _StoresDB.Stocks.Remove(stock);
        }
        /// <summary>
        /// get all Stock list
        /// </summary>
        /// <returns></returns>
        public List<Stock> GetAll()
        {
            List<Stock> list = _StoresDB.Stocks.Include(i => i.Manger).ToList();

            return list;
        }
        /// <summary>
        /// get stocks list paginated and filtered
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNumber"></param>
        /// <param name="FilterdJsonObj"></param>
        /// <returns></returns>
        public List<Stock> GetAllPaged(int PageSize, int PageNumber, FilterStockDTO? FilterdJsonObj)
        {
            int skiip = (PageNumber - 1) * PageSize;

            List<Stock> list = _StoresDB.Stocks.Include(u => u.Manger).Where(s =>
            (String.IsNullOrEmpty(FilterdJsonObj.NameOrAddress) || s.Name.ToLower().Contains(FilterdJsonObj.NameOrAddress.ToLower()) || s.Address.ToLower().Contains(FilterdJsonObj.NameOrAddress.ToLower())) &&
               (FilterdJsonObj.MangerIds == null || FilterdJsonObj.MangerIds.Count == 0 || FilterdJsonObj.MangerIds.Contains(s.MangerId.GetValueOrDefault()))
              ).Skip(skiip).Take(PageSize).ToList();

            return list;
        }
        /// <summary>
        ///  get filtered stocks count
        /// </summary>
        /// <param name="FilterdJsonObj"></param>
        /// <returns></returns>
        public int GetAllTotalCount(FilterStockDTO? FilterdJsonObj)
        {
            int count = _StoresDB.Stocks.Where(s =>
            (String.IsNullOrEmpty(FilterdJsonObj.NameOrAddress) || s.Name.ToLower().Contains(FilterdJsonObj.NameOrAddress.ToLower()) || s.Address.ToLower().Contains(FilterdJsonObj.NameOrAddress.ToLower())) &&
               (FilterdJsonObj.MangerIds == null || FilterdJsonObj.MangerIds.Count == 0 || FilterdJsonObj.MangerIds.Contains(s.MangerId.GetValueOrDefault()))
              ).Count();

            return count;
        }

        /// <summary>
        /// get StockDTO full details by stock id
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public StockDTO? GetById(int stockId)
        {
            StockDTO? stock = (from stockk in _StoresDB.Stocks
                               join user in _StoresDB.Users
                               on stockk.MangerId equals user.UserId into manger
                               from stockManger in manger.DefaultIfEmpty()
                               where stockk.StockId == stockId
                               select new { stockk, stockManger }).Select(tuple => new StockDTO
                               {
                                   StockId = tuple.stockk.StockId,
                                   Name = tuple.stockk.Name,
                                   Address = tuple.stockk.Address,
                                   MangerId = tuple.stockk.MangerId,
                                   Manger = new UserDTO()
                                   {
                                       UserId = tuple.stockManger.UserId,
                                       Name = tuple.stockManger.Name,
                                       Email = tuple.stockManger.Email,
                                       Fax = tuple.stockManger.Fax,
                                       Phone = tuple.stockManger.Phone,
                                       Website = tuple.stockManger.Website
                                   }

                               }).FirstOrDefault();

            return stock;

        }

        /// <summary>
        /// get stock entity full details by id
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public Stock? GetEntityById(int stockId)
        {

            Stock? stock = _StoresDB.Stocks
                .Include(x => x.StockItems)
                .Include(x => x.SupplyRequests).ThenInclude(y => y.RequestItems)
                .Include(x => x.WithdrawRequests).ThenInclude(y => y.RequestItems)
                .FirstOrDefault(s => s.StockId == stockId);

            return stock;

        }

        /// <summary>
        /// edit stock database
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="stock"></param>
        /// <returns></returns>
        public bool EditStock(int stockId, Stock stock)
        {
            Stock foundStock = _StoresDB.Stocks.Find(stockId);

            if (foundStock == null)
            {
                return false;
            }
            else
            {
                _StoresDB.Entry(foundStock).State = EntityState.Detached;
            }

            _StoresDB.Attach(stock);
            _StoresDB.Entry(stock).State = EntityState.Modified;

            return true;

        }

    }
}
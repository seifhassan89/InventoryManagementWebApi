using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Repositories.Contracts;
using System.Security.Cryptography;

namespace Repositories
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        private StocksDB _StoresDB;
        public ItemRepository(StocksDB StoresDB) : base(StoresDB)
        {

            _StoresDB = StoresDB;
        }
        /// <summary>
        /// add item to database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Item AddItem(Item item)
        {
            EntityEntry<Item> addedItem = _StoresDB.Items.Add(item);

            return addedItem.Entity;
        }

        /// <summary>
        /// delete item from database
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public void DeleteItem(Item item)
        {
            _StoresDB.Items.Remove(item);
        }

        /// <summary>
        /// get filtered items list
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        public List<Item> GetAll(FilterItemDTO? jsonObj)
        {
            List<Item> list = _StoresDB.Items
                .Include(x => x.MeasuringUnit)
                .Where(i =>

            (String.IsNullOrEmpty(jsonObj.NameOrCodeOrMeasuringUnitName) ||
            i.Name.ToLower().Contains(jsonObj.NameOrCodeOrMeasuringUnitName.ToLower()) ||
            i.Code.Contains(jsonObj.NameOrCodeOrMeasuringUnitName) ||
            i.MeasuringUnit.Name.Contains(jsonObj.NameOrCodeOrMeasuringUnitName)) &&

            (jsonObj.MeasuringUnitId == null ||
            jsonObj.MeasuringUnitId.Count == 0 ||
            jsonObj.MeasuringUnitId.Contains(i.MeasuringUnitId))
            ).ToList();

            return list;
        }

        /// <summary>
        /// get items list paginated and filtered
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        public List<Item> GetAllPaged(int currentPage, int pageSize, FilterItemDTO jsonObj)
        {

            List<Item> list = _StoresDB.Items.Include(x => x.MeasuringUnit).Where(i =>

            (String.IsNullOrEmpty(jsonObj.NameOrCodeOrMeasuringUnitName) ||
            i.Name.ToLower().Contains(jsonObj.NameOrCodeOrMeasuringUnitName.ToLower()) ||
            i.Code.Contains(jsonObj.NameOrCodeOrMeasuringUnitName) ||
            i.MeasuringUnit.Name.Contains(jsonObj.NameOrCodeOrMeasuringUnitName)) &&

            (jsonObj.MeasuringUnitId == null ||
            jsonObj.MeasuringUnitId.Count == 0 ||
            jsonObj.MeasuringUnitId.Contains(i.MeasuringUnitId))

            ).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return list;

        }

        /// <summary>
        /// all items count
        /// </summary>
        /// <returns></returns>
        public int GetAllCount(FilterItemDTO jsonObj)
        {
            int count = _StoresDB.Items.Where(i =>

            (String.IsNullOrEmpty(jsonObj.NameOrCodeOrMeasuringUnitName) ||
            i.Name.ToLower().Contains(jsonObj.NameOrCodeOrMeasuringUnitName.ToLower()) ||
            i.Code.Contains(jsonObj.NameOrCodeOrMeasuringUnitName) ||
            i.MeasuringUnit.Name.Contains(jsonObj.NameOrCodeOrMeasuringUnitName)) &&

            (jsonObj.MeasuringUnitId == null ||
            jsonObj.MeasuringUnitId.Count == 0 ||
            jsonObj.MeasuringUnitId.Contains(i.MeasuringUnitId))

            ).Count();
            return count;
        }

        /// <summary>
        /// get item by id full details
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ItemDTO? GetByIdFullDetails(int itemId)
        {
            ItemDTO? item = (from itemm in _StoresDB.Items

                             join measure in _StoresDB.MeasuringUnits
                             on itemm.MeasuringUnitId equals measure.MeasuringUnitId into itemMeasuringUnitRelation
                             from itemMeasuringUnit in itemMeasuringUnitRelation.DefaultIfEmpty()

                             join stockItem in _StoresDB.StockItems
                             on itemm.ItemId equals stockItem.ItemId into stockItemRelation
                             from stockItem in stockItemRelation.DefaultIfEmpty()

                             join stock in _StoresDB.Stocks
                             on stockItem.StockId equals stock.StockId into stockRelation
                             from stock in stockRelation.DefaultIfEmpty()

                             where itemm.ItemId == itemId
                             select new { itemm, itemMeasuringUnit, stock })
                     .GroupBy(i => new
                     {
                         itemId = i.itemm.ItemId,
                         itemName = i.itemm.Name,
                         itemCode = i.itemm.Code,
                         measuringUnitId = i.itemMeasuringUnit.MeasuringUnitId,
                         measuringUnitName = i.itemMeasuringUnit.Name
                     })
                     .Select(i => new ItemDTO()
                     {
                         ItemId = i.Key.itemId,
                         Name = i.Key.itemName,
                         Code = i.Key.itemCode,
                         MeasuringUnitId = i.Key.measuringUnitId,
                         MeasuringUnitName = i.Key.measuringUnitName,
                         Stocks = i.Where(s => s.stock != null).Select(s => new StockDTO()
                         {
                             StockId = s.stock.StockId,
                             Name = s.stock.Name
                         }).ToList(),

                     }).FirstOrDefault();

            return item;


        }

        /// <summary>
        /// get item entity by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Item? GetById(int itemId)
        {
            Item? itemEntity = (from item in _StoresDB.Items
                                where item.ItemId == itemId
                                select item)
                    .FirstOrDefault();

            return itemEntity;


        }

        /// <summary>
        /// edit item data in database
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool EditItem(int itemId, Item item)
        {
            Item? existingEntity = _StoresDB.Items.Find(itemId);
            if (existingEntity == null)
            {
                return false;
            }
            else
            {
                _StoresDB.Entry(existingEntity).State = EntityState.Detached;
            }

            _StoresDB.Attach(item);
            _StoresDB.Entry(item).State = EntityState.Modified;

            return true;

        }
    }
}
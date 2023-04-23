using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Repositories.Contracts;

namespace Repositories
{
    public class MeasuringUnitRepository : BaseRepository, IMeasuringUnitRepository
    {
        private StocksDB _StoresDB;
        public MeasuringUnitRepository(StocksDB StoresDB) : base(StoresDB)
        {

            _StoresDB = StoresDB;
        }
        /// <summary>
        /// add measuring unit to db
        /// </summary>
        /// <param name="measuringUnit"></param>
        /// <returns></returns>
        public MeasuringUnit AddMeasuringUnit(MeasuringUnit measuringUnit)
        {
            EntityEntry<MeasuringUnit> addedMeasuringUnit = _StoresDB.MeasuringUnits.Add(measuringUnit);

            return addedMeasuringUnit.Entity;
        }
        /// <summary>
        /// delete mesuaring unit from db
        /// </summary>
        /// <param name="measuringUnitId"></param>
        /// <returns></returns>
        public bool DeleteMeasuringUnit(int measuringUnitId)
        {
            MeasuringUnit? foundMeasuringUnit = _StoresDB.MeasuringUnits.Find(measuringUnitId);
            if (foundMeasuringUnit != null)
            {
                _StoresDB.MeasuringUnits.Remove(foundMeasuringUnit);
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// get all measuring units
        /// </summary>
        /// <returns></returns>
        public List<MeasuringUnit> GetAll()
        {
            List<MeasuringUnit> list = _StoresDB.MeasuringUnits.ToList();
            return list;
        }
        /// <summary>
        /// get measuring unit by id
        /// </summary>
        /// <param name="measuringUnitId"></param>
        /// <returns></returns>
        public MeasuringUnit? GetById(int measuringUnitId)
        {
            MeasuringUnit? measuringUnit = _StoresDB.MeasuringUnits.FirstOrDefault(m => m.MeasuringUnitId == measuringUnitId);
            return measuringUnit;
        }
        /// <summary>
        /// update measuring unit data
        /// </summary>
        /// <param name="measuringUnitId"></param>
        /// <param name="measuringUnit"></param>
        /// <returns></returns>
        public bool EditMeasuringUnit(int measuringUnitId, MeasuringUnit measuringUnit)
        {
            MeasuringUnit? foundMeasuringUnit = _StoresDB.MeasuringUnits.Find(measuringUnitId);

            if (foundMeasuringUnit != null)
            {
                _StoresDB.Entry(foundMeasuringUnit).State = EntityState.Detached;
            }
            else
            {
                return false;
            }

            _StoresDB.Attach(measuringUnit);
            _StoresDB.Entry(measuringUnit).State = EntityState.Modified;

            return true;

        }
    }
}
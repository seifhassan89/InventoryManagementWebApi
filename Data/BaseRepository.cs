using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly StocksDB _stockDB;
        public BaseRepository(StocksDB stocksDB)
        {
            _stockDB = stocksDB;
        }

        public void SaveChanges()
        {
            _stockDB.SaveChanges();
        }
    }
}

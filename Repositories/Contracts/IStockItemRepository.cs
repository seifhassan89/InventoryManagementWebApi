using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IStockItemRepository : IBaseRepository
    {
        bool DeleteStockItem(StockItem? stockItem);


    }
}

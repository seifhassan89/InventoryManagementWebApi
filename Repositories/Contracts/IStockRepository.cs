using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IStockRepository : IBaseRepository
    {
        List<Stock> GetAll();
        List<Stock> GetAllPaged(int PageSize, int PageNumber, FilterStockDTO? JsonObj);
        int GetAllTotalCount(FilterStockDTO? JsonObj);

        StockDTO? GetById(int stockId);
        Stock? GetEntityById(int stockId);

        void DeleteStock(Stock stock);

        Stock AddStock(Stock stock);

        bool EditStock(int stockId, Stock stock);

    }
}

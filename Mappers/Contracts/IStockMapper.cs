using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.Contracts
{
    public interface IStockMapper
    {
        Stock MapToStock(StockDTO stockDTO);

        StockDTO MapToStockDTO(Stock stock);
    }
}

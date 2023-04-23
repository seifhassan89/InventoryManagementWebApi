using Models;
using Mappers.Contracts;

namespace Mappers
{
    public class StockMapper : IStockMapper
    {
        public Stock MapToStock(StockDTO stockDTO)
        {
            Stock stock = new Stock();
            stock.StockId = stockDTO.StockId;
            stock.Name = stockDTO.Name;
            stock.Address = stockDTO.Address;
            stock.MangerId = stockDTO.MangerId;
            if(stockDTO.Manger != null) {
                stock.Manger = new User();
                stock.Manger.Name = stockDTO.Manger.Name;
                stock.Manger.UserId = stockDTO.Manger.UserId;
                stock.Manger.Email= stockDTO.Manger.Email;
                stock.Manger.Fax = stockDTO.Manger.Fax;
                stock.Manger.Phone = stockDTO.Manger.Phone;
                stock.Manger.Website= stockDTO.Manger.Website;
            }

            return stock;
        }

        public StockDTO MapToStockDTO(Stock stock)
        {
            StockDTO stockDTO = new StockDTO();
            stockDTO.StockId = stock.StockId;
            stockDTO.Name = stock.Name;
            stockDTO.Address = stock.Address;
            stockDTO.MangerId = stock.MangerId;
            if (stock.Manger != null)
            {
                stockDTO.Manger = new UserDTO();
                stockDTO.Manger.Name = stock.Manger.Name;
                stockDTO.Manger.UserId = stock.Manger.UserId;
                stockDTO.Manger.Email = stock.Manger.Email;
                stockDTO.Manger.Fax = stock.Manger.Fax;
                stockDTO.Manger.Phone = stock.Manger.Phone;
                stockDTO.Manger.Website = stock.Manger.Website;
            }

            return stockDTO;
        }
    }
}
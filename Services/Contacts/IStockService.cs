using employee_task.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IStockService
    {
        public List<StockDTO> GetAll();
        public PagedResultDTO<StockDTO> GetAllPaged(int pageSize, int pageNumber, string? filterdObjJson);
        public StockDTO? GetById(int id);
        public StockDTO AddStock(StockDTO stock);
        public bool EditStock(int stockId, StockDTO stockDTO);
        public bool DeleteStock(int stockId);
        public void SaveChanges();

        public List<string> checkValidation(int? id, StockDTO stockDTO);
    }
}

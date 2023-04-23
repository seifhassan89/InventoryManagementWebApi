using employee_task.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IItemService
    {
        public List<ItemDTO> GetAll(string? filterObjectJson);
        public PagedResultDTO<ItemDTO> GetAllPaged(int currentPage, int pageSize, string? filterObjectJson);
        public ItemDTO? GetById(int id);
        public ItemDTO AddItem(ItemDTO itemDto);
        public bool EditItem(int itemId, ItemDTO itemDTO);
        public bool DeleteItem(int itemId);
        public void SaveChanges();

        public List<string> CheckItemValidation(ItemDTO itemDTO);

    }
}

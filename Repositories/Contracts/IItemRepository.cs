using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IItemRepository : IBaseRepository
    {
        List<Item> GetAll(FilterItemDTO? jsonObj);

        List<Item> GetAllPaged(int currentPage, int pageSize, FilterItemDTO jsonObj);

        int GetAllCount(FilterItemDTO jsonObj);

        ItemDTO? GetByIdFullDetails(int itemId);

        void DeleteItem(Item item);

        Item AddItem(Item item);
        Item? GetById(int itemId);
        bool EditItem(int itemId, Item item);

    }
}

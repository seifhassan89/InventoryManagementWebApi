using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.Contracts
{
    public interface IItemMapper
    {
        Item MapToItem(ItemDTO itemDTO);

        ItemDTO MapToItemDTO(Item item);
    }
}

using Models;
using Mappers.Contracts;

namespace Mappers
{
    public class ItemMapper : IItemMapper
    {
        public Item MapToItem(ItemDTO itemDTO)
        {
            Item item = new Item();
            item.ItemId = itemDTO.ItemId;
            item.Name = itemDTO.Name;
            item.Code = itemDTO.Code;
            item.MeasuringUnitId = itemDTO.MeasuringUnitId;
            //item.StockItems
            //item.RequestItems
            //item.MeasuringUnit
            return item;
        }

        public ItemDTO MapToItemDTO(Item item)
        {
            ItemDTO itemDTO = new ItemDTO();
            itemDTO.ItemId = item.ItemId;
            itemDTO.Name = item.Name;
            itemDTO.Code = item.Code;
            itemDTO.MeasuringUnitId = item.MeasuringUnitId;
            itemDTO.MeasuringUnitName = item.MeasuringUnit?.Name ?? "";
            return itemDTO;
        }
    }
}
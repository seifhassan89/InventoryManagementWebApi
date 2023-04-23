using employee_task.Models;
using Newtonsoft.Json;
using Models;
using Mappers.Contracts;
using Repositories.Contracts;
using Services.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly IItemMapper _itemMapper;
        private readonly IItemRepository _itemRepository;


        public ItemService(IItemMapper itemMapper, IItemRepository itemRepository)
        {
            _itemMapper = itemMapper;
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// add item to Database
        /// </summary>
        /// <param name="itemDto"></param>
        /// <returns></returns>
        public ItemDTO AddItem(ItemDTO itemDto)
        {

            Item item = _itemMapper.MapToItem(itemDto);
            Item AddedItem = _itemRepository.AddItem(item);
            ItemDTO addedItem = _itemMapper.MapToItemDTO(AddedItem);
            return addedItem;
        }

        /// <summary>
        /// delete item by id from Database
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool DeleteItem(int itemId)
        {
            Item? item = _itemRepository.GetById(itemId); ;
            if (item == null)
            {
                return false;
            }
            _itemRepository.DeleteItem(item);
            return true;
        }

        /// <summary>
        /// edit item data
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemDTO"></param>
        /// <returns></returns>
        public bool EditItem(int itemId, ItemDTO itemDTO)
        {
            Item item = _itemMapper.MapToItem(itemDTO);
            return _itemRepository.EditItem(itemId, item);
        }

        /// <summary>
        /// get items list filtered from Database
        /// </summary>
        /// <param name="filterObjectJson"></param>
        /// <returns></returns>
        public List<ItemDTO> GetAll(string? filterObjectJson)
        {
            List<ItemDTO> mappedList = new List<ItemDTO>();

            FilterItemDTO JsonObj = new FilterItemDTO();


            if (!String.IsNullOrEmpty(filterObjectJson))
            {
                JsonObj = JsonConvert.DeserializeObject<FilterItemDTO>(filterObjectJson);
            }

            List<Item> listOfItem = _itemRepository.GetAll(JsonObj);


            listOfItem.ForEach(item =>
            {
                ItemDTO itemDTO = _itemMapper.MapToItemDTO(item);
                mappedList.Add(itemDTO);
            });

            return mappedList;
        }

        /// <summary>
        /// get items list filtered and paginated from Database
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="FilterObjJson"></param>
        /// <returns></returns>
        public PagedResultDTO<ItemDTO> GetAllPaged(int page, int pageSize, string? FilterObjJson)
        {
            PagedResultDTO<ItemDTO> pagedResultDTO = new PagedResultDTO<ItemDTO>();

            FilterItemDTO JsonObj = new FilterItemDTO();

            if (!String.IsNullOrEmpty(FilterObjJson))
            {
                JsonObj = JsonConvert.DeserializeObject<FilterItemDTO>(FilterObjJson);
            }


            List<ItemDTO> mappedList = new List<ItemDTO>();


            List<Item> listOfItem = _itemRepository.GetAllPaged(page, pageSize, JsonObj);

            listOfItem.ForEach(item =>
            {
                ItemDTO itemDTO = _itemMapper.MapToItemDTO(item);
                mappedList.Add(itemDTO);
            });

            pagedResultDTO.List = mappedList;
            pagedResultDTO.TotalRecords = _itemRepository.GetAllCount(JsonObj);
            pagedResultDTO.PageNumber = page;

            return pagedResultDTO;

        }

        /// <summary>
        /// get item full details by id from Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ItemDTO? GetById(int id)
        {
            ItemDTO? item = _itemRepository.GetByIdFullDetails(id);

            return item;

        }

        public void SaveChanges()
        {
            _itemRepository.SaveChanges();
        }

        /// <summary>
        /// validate item data
        /// </summary>
        /// <param name="itemDTO"></param>
        /// <returns></returns>
        public List<string> CheckItemValidation(ItemDTO itemDTO)
        {

            List<string> errors = new List<string>();
            if (String.IsNullOrEmpty(itemDTO.Name))
            {
                errors.Add("Name is required");
            }
            if (String.IsNullOrEmpty(itemDTO.Code))
            {
                errors.Add("Code is required");
            }
            if (itemDTO.MeasuringUnitId == 0)
            {
                errors.Add("MeasuringUnitId is required");
            }

            return errors;
        }
    }
}

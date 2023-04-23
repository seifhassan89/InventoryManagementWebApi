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
    public class StockService : IStockService
    {

        private readonly IStockRepository _stockRepository;
        private readonly IStockMapper _stockMapper;
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IRequestItemRepository _requestItemRepository;

        public StockService(IStockMapper stockMapper, IRequestRepository requestRepository, IStockRepository stockRepository, IStockItemRepository stockItemRepository, IRequestItemRepository requestItemRepository)
        {
            _stockMapper = stockMapper;
            _stockRepository = stockRepository;
            _stockItemRepository = stockItemRepository;
            _requestRepository = requestRepository;
            _requestItemRepository = requestItemRepository;


        }
        /// <summary>
        /// add stock to db
        /// </summary>
        /// <param name="stockDTO"></param>
        /// <returns></returns>
        public StockDTO AddStock(StockDTO stockDTO)
        {
            Stock stock = _stockMapper.MapToStock(stockDTO);
            Stock AddedStock = _stockRepository.AddStock(stock);
            StockDTO addedStock = _stockMapper.MapToStockDTO(AddedStock);
            return addedStock;
        }
        /// <summary>
        /// delete stock with related objects from DB
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public bool DeleteStock(int stockId)
        {

            Stock? stockEntity = _stockRepository.GetEntityById(stockId);

            if (stockEntity == null)
            {
                return false;
            }
            else
            {
                stockEntity.StockItems.ForEach(stockItem => _stockItemRepository.DeleteStockItem(stockItem));

                if (stockEntity.SupplyRequests.Count > 0)
                {
                    stockEntity.SupplyRequests.ForEach(req =>
                    {
                        deleteItemsForReq(req);
                    });
                }

                if (stockEntity.WithdrawRequests.Count > 0)
                {
                    stockEntity.WithdrawRequests.ForEach(req =>
                    {
                        deleteItemsForReq(req);
                    });
                }
            }
            _stockRepository.DeleteStock(stockEntity);

            return true;


        }

        /// <summary>
        /// delete request iems
        /// </summary>
        /// <param name="req"></param>
        private void deleteItemsForReq(Request req)
        {
            bool isDeleted = false;
            req.RequestItems.ForEach(item =>
            {
                isDeleted = _requestItemRepository.DeleteRequestItem(item);
            });
            _requestRepository.DeleteRequest(req);

        }

        /// <summary>
        /// edit stock data
        /// </summary>
        /// <param name="stockId"></param>
        /// <param name="stockDTO"></param>
        /// <returns></returns>
        public bool EditStock(int stockId, StockDTO stockDTO)
        {
            Stock stock = _stockMapper.MapToStock(stockDTO);
            return _stockRepository.EditStock(stockId, stock);

        }

        /// <summary>
        /// get all stocks list
        /// </summary>
        /// <returns></returns>
        public List<StockDTO> GetAll()
        {

            List<StockDTO> mappedList = new List<StockDTO>();

            List<Stock> listOfStocks = _stockRepository.GetAll();

            listOfStocks.ForEach(stock =>
            {
                StockDTO stockDTO = _stockMapper.MapToStockDTO(stock);
                mappedList.Add(stockDTO);
            });

            return mappedList;
        }

        /// <summary>
        /// get stocks list paginated and filtered
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filterdObjJson"></param>
        /// <returns></returns>
        public PagedResultDTO<StockDTO> GetAllPaged(int pageSize, int pageNumber, string? filterdObjJson)
        {
            PagedResultDTO<StockDTO> result = new PagedResultDTO<StockDTO>();

            List<StockDTO> mappedList = new List<StockDTO>();

            FilterStockDTO JsonObj = new FilterStockDTO();

            if (!String.IsNullOrEmpty(filterdObjJson))
            {
                JsonObj = JsonConvert.DeserializeObject<FilterStockDTO>(filterdObjJson);
            }


            List<Stock> stocks = _stockRepository.GetAllPaged(pageSize, pageNumber, JsonObj);

            stocks.ForEach(stock =>
            {

                StockDTO stockDTO = _stockMapper.MapToStockDTO(stock);

                mappedList.Add(stockDTO);

            });

            result.List = mappedList;

            result.PageNumber = pageNumber;

            result.TotalRecords = _stockRepository.GetAllTotalCount(JsonObj);

            return result;
        }

        /// <summary>
        /// get stock DTO full details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StockDTO? GetById(int id)
        {
            StockDTO? stock = _stockRepository.GetById(id);
            return stock;
        }

        public void SaveChanges()
        {
            _stockRepository.SaveChanges();
        }

        /// <summary>
        /// check stock validation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stockDTO"></param>
        /// <returns></returns>
        public List<string> checkValidation(int? id, StockDTO stockDTO)
        {
            List<string> errors = new List<string>();

            if (stockDTO == null)
            {
                errors.Add("please send Stock");
            }
            if (String.IsNullOrEmpty(stockDTO.Name))
            {
                errors.Add("please add a name of stock!");
            }
            if (String.IsNullOrEmpty(stockDTO.Address))
            {
                errors.Add("please add a Address of stock!");
            }
            //if (stockDTO.MangerId == null)
            //{
            //    errors.Add("please add manager id for this stock");
            //}
            if (id != null)
            {
                if (stockDTO.StockId == 0)
                {
                    errors.Add("please send stockId");
                }
                if (stockDTO.StockId != id)
                {
                    errors.Add("StockId and id sould be identical!");
                }
            }

            return errors;

        }


    }
}

using employee_task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contacts;
using System.Web.Http.Results;


namespace Controllers
{
    [ApiController]
    [Route("Stocks")]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {

            _stockService = stockService;
        }

        /// <summary>
        /// get all stocks list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            ResultDTO<List<StockDTO>> result = new ResultDTO<List<StockDTO>>();
            result.Results = _stockService.GetAll();
            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }
        /// <summary>
        /// get stock list paginated and filtered
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="filterObjectJson"></param>
        /// <returns></returns>
        [HttpGet("paged")]
        public ActionResult GetAllPaged([FromQuery] int currentPage, [FromQuery] int pageSize, [FromQuery] string? filterObjectJson)
        {
            PagedResultDTO<StockDTO> result = _stockService.GetAllPaged(pageSize, currentPage, filterObjectJson);

            return Ok(result);
        }

        /// <summary>
        /// get stock Data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            ResultDTO<StockDTO> result = new ResultDTO<StockDTO>();

            StockDTO? foundUser = _stockService.GetById(id);
            if (foundUser != null)
            {
                result.Results = foundUser;
                result.StatusCode = Ok().StatusCode;
                return Ok(result);
            }
            else
            {
                result.ErrorsMessages.Add("Stock Not Found !");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);
            }

        }
        /// <summary>
        /// add stock to Database
        /// </summary>
        /// <param name="stockDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddStock([FromBody] StockDTO stockDTO)
        {
            ResultDTO<StockDTO> result = new ResultDTO<StockDTO>();
            if (!ModelState.IsValid)
            {
                result.ErrorsMessages.Add("Please Add valid Stock");
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }

            List<string> error = _stockService.checkValidation(null, stockDTO);
            if (error.Count > 0)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages = error;
                return BadRequest(result);
            }
            result.Results = _stockService.AddStock(stockDTO);
            result.StatusCode = Ok().StatusCode;
            result.Messages.Add("Added successful");
            _stockService.SaveChanges();
            return Ok(result);
        }

        /// <summary>
        /// edit stock data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stockDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult EditStock([FromRoute] int id, [FromBody] StockDTO stockDTO)
        {
            ResultDTO<StockDTO> result = new ResultDTO<StockDTO>();
            if (!ModelState.IsValid)
            {
                result.ErrorsMessages.Add("Please Add valid Stock");
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }

            List<string> error = _stockService.checkValidation(id, stockDTO);
            if (error.Count > 0)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages = error;
                return BadRequest(result);
            }
            bool isEdited = _stockService.EditStock(id, stockDTO);
            if (!isEdited)
            {
                result.ErrorsMessages.Add("Stock Not Found!!!!!!");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);

            }
            result.Results = stockDTO;
            result.StatusCode = Ok().StatusCode;
            result.Messages.Add("Stock Updated successfully !");
            _stockService.SaveChanges();
            return Ok(result);
        }

        /// <summary>
        /// delete stock from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteStock([FromRoute] int id)
        {
            ResultDTO<StockDTO> result = new ResultDTO<StockDTO>();
            bool StockDeleted = _stockService.DeleteStock(id);
            if (StockDeleted)
            {

                result.StatusCode = Ok().StatusCode;
                result.Messages.Add("Deleted successful");
                _stockService.SaveChanges();
                return Ok(result);
            }
            else
            {
                result.StatusCode = NotFound().StatusCode;
                result.ErrorsMessages.Add("Stock not found");

                return NotFound(result);
            }
        }

    }
}

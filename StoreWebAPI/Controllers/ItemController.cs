using employee_task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contacts;

namespace Controllers
{
    [Route("Items")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        /// <summary>
        /// get filtered items list 
        /// </summary>
        /// <param name="filterObjectJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll([FromQuery] string? filterObjectJson)
        {

            ResultDTO<List<ItemDTO>> result = new ResultDTO<List<ItemDTO>>();
            result.Results = _itemService.GetAll(filterObjectJson);
            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }

        /// <summary>
        /// get items list filtered and paginated from Database
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="filterObjectJson"></param>
        /// <returns></returns>
        [HttpGet("paged")]
        public ActionResult GetAllPaged([FromQuery] int currentPage, [FromQuery] int pageSize, [FromQuery] string? filterObjectJson)
        {
            PagedResultDTO<ItemDTO> pagedResult = _itemService.GetAllPaged(currentPage, pageSize, filterObjectJson);
            return Ok(pagedResult);

        }

        /// <summary>
        /// get item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            ResultDTO<ItemDTO> result = new ResultDTO<ItemDTO>();
            ItemDTO? itemFound = _itemService.GetById(id);
            if (itemFound == null)
            {
                result.ErrorsMessages.Add("Item Not Found !");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);
            }
            else
            {
                result.Results = itemFound;
                result.StatusCode = Ok().StatusCode;
                return Ok(result);
            }
        }
        /// <summary>
        /// delete item by id from Db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteItem([FromRoute] int id)
        {
            ResultDTO<ItemDTO> result = new ResultDTO<ItemDTO>();
            bool isDeleted = _itemService.DeleteItem(id);
            if (isDeleted)
            {

                result.Messages.Add("Item Deleted Succefully !");
                result.StatusCode = Ok().StatusCode;
                _itemService.SaveChanges();
                return Ok(result);
            }
            else
            {
                result.ErrorsMessages.Add("Item not found !");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);
            }

        }
        /// <summary>
        /// edit item data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult EditItem([FromRoute] int id, [FromBody] ItemDTO itemDTO)
        {
            ResultDTO<ItemDTO> result = new ResultDTO<ItemDTO>();

            if (!ModelState.IsValid)
            {
                result.ErrorsMessages.Add("Please Add valid Item");
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }

            List<string> errorsMessages = _itemService.CheckItemValidation(itemDTO);

            if (errorsMessages.Count > 0)
            {
                result.ErrorsMessages = errorsMessages;
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }

            bool isEdited = _itemService.EditItem(id, itemDTO);
            if (!isEdited)
            {
                result.ErrorsMessages.Add("Item Not Found!!!!!!");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);

            }
            result.Results = itemDTO;
            result.Messages.Add("Item Updated successfully !");
            result.StatusCode = Ok().StatusCode;
            _itemService.SaveChanges();
            return Ok(result);
        }

        /// <summary>
        /// add item to database
        /// </summary>
        /// <param name="itemDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddItem([FromBody] ItemDTO itemDTO)
        {
            ResultDTO<ItemDTO> result = new ResultDTO<ItemDTO>();

            if (!ModelState.IsValid)
            {
                result.ErrorsMessages.Add("Please Add valid Item");
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }

            List<string> errorsMessages = _itemService.CheckItemValidation(itemDTO);

            if (errorsMessages.Count > 0)
            {
                result.ErrorsMessages = errorsMessages;
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }

            result.Results = _itemService.AddItem(itemDTO);
            result.Messages.Add("Added Succefully !");
            result.StatusCode = Ok().StatusCode;
            _itemService.SaveChanges();
            return Ok(result);
        }
    }
}

using employee_task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contacts;

namespace Controllers
{
    [Route("Requests")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }
        /// <summary>
        /// get requests list fitered
        /// </summary>
        /// <param name="filterObjectJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll([FromQuery] string? filterObjectJson)
        {

            ResultDTO<List<RequestDTO>> result = new ResultDTO<List<RequestDTO>>();
            result.Results = _requestService.GetAll(filterObjectJson);
            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }
        /// <summary>
        /// get requests list filtered and paginated from Database
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="filterObjectJson"></param>
        /// <returns></returns>
        [HttpGet("paged")]
        public ActionResult GetAllPaged([FromQuery] int currentPage, [FromQuery] int pageSize, [FromQuery] string? filterObjectJson)
        {

            PagedResultDTO<RequestDTO> pagedResultDTO = _requestService.GetAllPaged(currentPage, pageSize, filterObjectJson);
            return Ok(pagedResultDTO);
        }
        /// <summary>
        /// get request by id with full details 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            ResultDTO<RequestDTO> result = new ResultDTO<RequestDTO>();
            RequestDTO? foundRequest = _requestService.GetById(id);
            if (foundRequest == null)
            {
                result.ErrorsMessages.Add("Request Not Found !");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);
            }
            else
            {
                result.Results = foundRequest;
                result.StatusCode = Ok().StatusCode;
                return Ok(result);
            }
        }
        /// <summary>
        /// Delete request from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteRequest([FromRoute] int id)
        {
            ResultDTO<RequestDTO> result = new ResultDTO<RequestDTO>();
            bool isDeleted = _requestService.DeleteRequest(id);
            if (isDeleted)
            {

                result.Messages.Add("Request Deleted Succefully !");
                result.StatusCode = Ok().StatusCode;
                _requestService.SaveChanges();
                return Ok(result);
            }
            else
            {
                result.ErrorsMessages.Add("Request not found !");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);


            }
        }
        /// <summary>
        /// edit request data 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult EditRequest([FromRoute] int id, [FromBody] RequestDTO requestDTO)
        {

            ResultDTO<RequestDTO> result = new ResultDTO<RequestDTO>();
            if (!ModelState.IsValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("model is not valied!");
                return BadRequest(result);
            }
            List<string> errors = _requestService.Validation(id, requestDTO);
            if (errors.Count > 0)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages = errors;
                return BadRequest(result);
            }
            bool isEdited = _requestService.EditRequest(id, requestDTO);
            if (!isEdited)
            {
                result.ErrorsMessages.Add("Request Not Found!!!!!!");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);

            }
            result.Results = requestDTO;
            result.Messages.Add("Request Updated Succefully !");
            result.StatusCode = Ok().StatusCode;
            _requestService.SaveChanges();
            return Ok(result);
        }
        /// <summary>
        /// add request to database
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRequest([FromBody] RequestDTO requestDTO)
        {
            ResultDTO<RequestDTO> result = new ResultDTO<RequestDTO>();

            if (!ModelState.IsValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("model is not valied!");
                return BadRequest(result);
            }

            List<string> errors = _requestService.Validation(null, requestDTO);
            if (errors.Count > 0)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages = errors;
                return BadRequest(result);
            }

            result.Results = _requestService.AddRequest(requestDTO);
            result.Messages.Add("Reqeust Added Succefully !");
            result.StatusCode = Ok().StatusCode;
            _requestService.SaveChanges();
            return Ok(result);
        }
    }
}

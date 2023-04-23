using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contacts;

namespace Controllers
{
    [Route("RequestTypes")]
    [ApiController]
    public class RequestTypeController : Controller
    {
        private readonly IRequestTypeService _requestTypeService;

        public RequestTypeController(IRequestTypeService requestTypeService)
        {
            _requestTypeService = requestTypeService;
        }

        /// <summary>
        /// get all request types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {

            ResultDTO<List<RequestTypeDTO>> result = new ResultDTO<List<RequestTypeDTO>>();
            result.Results = _requestTypeService.GetAll();
            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }

        /// <summary>
        /// get reqyest type by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            ResultDTO<RequestTypeDTO> result = new ResultDTO<RequestTypeDTO>();
            RequestTypeDTO? foundRequestType = _requestTypeService.GetById(id);
            if (foundRequestType != null)
            {
                result.Results = foundRequestType;
                result.StatusCode = Ok().StatusCode;
                return Ok(result);
            }
            else
            {
                result.StatusCode = NotFound().StatusCode;
                result.ErrorsMessages.Add("Request Type not Found!");

                return NotFound(result);

            }
        }

        /// <summary>
        /// delete request type by id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteRequestType([FromRoute] int id)
        {
            ResultDTO<RequestTypeDTO> result = new ResultDTO<RequestTypeDTO>();

            bool isDeleted = _requestTypeService.DeleteRequestType(id);
            if (isDeleted)
            {

                result.Messages.Add("Deleted Succefully !");
                result.StatusCode = Ok().StatusCode;
                _requestTypeService.SaveChanges();
                return Ok(result);
            }
            else
            {
                result.ErrorsMessages.Add("not Found");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);
            }

        }

        /// <summary>
        /// update request type data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestTypeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult EditRequestType([FromRoute] int id, [FromBody] RequestTypeDTO requestTypeDTO)
        {

            ResultDTO<RequestTypeDTO> result = new ResultDTO<RequestTypeDTO>();
            if (!ModelState.IsValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("please add a valid Request Type !");
                return BadRequest(result);
            }

            List<String> errors = _requestTypeService.CheckEditValidation(id, requestTypeDTO);
            if (errors.Count > 0)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages = errors;
                return BadRequest(result);
            }

            bool isUpdated = _requestTypeService.EditRequestType(id, requestTypeDTO);
            if (!isUpdated)
            {
                result.ErrorsMessages.Add("Request Type Not Found!!!!!!");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);
            }
            result.Messages.Add("Request Type Edited Succefully !");
            result.StatusCode = Ok().StatusCode;
            _requestTypeService.SaveChanges();
            return Ok(result);
        }

        /// <summary>
        /// add request type to db
        /// </summary>
        /// <param name="requestTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRequestType([FromBody] RequestTypeDTO requestTypeDTO)
        {
            ResultDTO<RequestTypeDTO> result = new ResultDTO<RequestTypeDTO>();

            if (!ModelState.IsValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("please add a valid Request Type !");
                return BadRequest(result);
            }

            bool isDataValid = _requestTypeService.CheckValidation(requestTypeDTO);

            if (!isDataValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("Name Can't be Empty !");
                return BadRequest(result);
            }

            result.Results = _requestTypeService.AddRequestType(requestTypeDTO);
            result.Messages.Add("Added Succefully !");
            result.StatusCode = Ok().StatusCode;
            _requestTypeService.SaveChanges();
            return Ok(result);
        }
    }
}

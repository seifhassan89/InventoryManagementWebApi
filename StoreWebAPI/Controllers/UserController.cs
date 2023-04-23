


using employee_task.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contacts;

namespace Controllers
{

    [ApiController]
    [Route("Users")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {

            _userService = userService;
        }
        /// <summary>
        /// get users list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            ResultDTO<List<UserDTO>> result = new ResultDTO<List<UserDTO>>();
            result.Results = _userService.GetAll();
            result.StatusCode = Ok().StatusCode;
            return Ok(result);

        }

        /// <summary>
        /// get users list paginated and filtered
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="filterObjectJson"></param>
        /// <returns></returns>
        [HttpGet("paged")]
        public ActionResult GetAllPaged([FromQuery] int currentPage, [FromQuery] int pageSize, [FromQuery] string? filterObjectJson)
        {
            PagedResultDTO<UserDTO> result = _userService.GetAllPaged(currentPage, pageSize, filterObjectJson);
            return Ok(result);

        }

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            ResultDTO<UserDTO> result = new ResultDTO<UserDTO>();

            UserDTO? foundedUser = _userService.GetById(id);

            if (foundedUser != null)
            {
                result.Results = foundedUser;
                result.StatusCode = Ok().StatusCode;
                return Ok(result);
            }
            else
            {
                result.ErrorsMessages.Add("There is no user found with this id");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);
            }
        }

        /// <summary>
        /// add user to database
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUser([FromBody] UserDTO userDTO)
        {
            ResultDTO<UserDTO> result = new ResultDTO<UserDTO>();

            if (!ModelState.IsValid)
            {
                result.ErrorsMessages.Add("Please Add valid User");
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }

            List<String> errors = _userService.GetValidation(null, userDTO);

            if (errors.Count > 0)
            {
                result.ErrorsMessages = errors;
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }

            result.Results = _userService.AddUser(userDTO);
            result.Messages.Add("Added Succefully !");
            result.StatusCode = Ok().StatusCode;
            _userService.SaveChanges();
            return Ok(result);
        }

        /// <summary>
        /// edit user data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult EditUser([FromRoute] int id, [FromBody] UserDTO userDTO)
        {
            ResultDTO<UserDTO> result = new ResultDTO<UserDTO>();

            if (!ModelState.IsValid)
            {
                result.ErrorsMessages.Add("Please Add valid User");
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }
            List<String> errors = _userService.GetValidation(id, userDTO);

            if (errors.Count > 0)
            {
                result.ErrorsMessages = errors;
                result.StatusCode = BadRequest().StatusCode;
                return BadRequest(result);
            }

            bool isEdited = _userService.EditUser(id, userDTO);
            if (!isEdited)
            {
                result.ErrorsMessages.Add("User Not Found!!!!!!");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);

            }
            result.Results = userDTO;
            result.Messages.Add("User Updated successfully !");
            result.StatusCode = Ok().StatusCode;
            _userService.SaveChanges();
            return Ok(result);
        }
        /// <summary>
        /// delete user from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            ResultDTO<UserDTO> result = new ResultDTO<UserDTO>();
            bool isUserDeleted = _userService.DeleteUser(id);
            if (isUserDeleted)
            {
                _userService.SaveChanges();
                result.Messages.Add("User Deleted Succefully !");
                result.StatusCode = Ok().StatusCode;
                return Ok(result);
            }
            else
            {
                result.ErrorsMessages.Add("User Not Found!");
                result.StatusCode = NotFound().StatusCode;
                return Ok(result);

            }
        }

    }
}

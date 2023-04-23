using Mappers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contacts;
using System.Web.Http.Results;


namespace Controllers
{
    [ApiController]
    [Route("Roles")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IRoleMapper _roleMapper;

        public RoleController(IRoleService roleService, IRoleMapper roleMapper)
        {

            _roleService = roleService;
            _roleMapper = roleMapper;

        }

        /// <summary>
        /// get Roles list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            ResultDTO<List<RoleDTO>> result = new ResultDTO<List<RoleDTO>>();
            result.Results = _roleService.GetAll();
            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }

        /// <summary>
        /// get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            ResultDTO<RoleDTO> result = new ResultDTO<RoleDTO>();
            RoleDTO? FoundRole = _roleService.GetById(id);
            if (FoundRole == null)
            {
                result.StatusCode = NotFound().StatusCode;
                result.ErrorsMessages.Add("Role not found");
                return NotFound(result);
            }
            else
            {
                result.Results = FoundRole;
                result.StatusCode = Ok().StatusCode;
                return Ok(result);
            }

        }
        /// <summary>
        /// add role to Db
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRole([FromBody] RoleDTO roleDTO)
        {
            ResultDTO<RoleDTO> result = new ResultDTO<RoleDTO>();
            if (!ModelState.IsValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("please add a valid role !");
                return BadRequest(result);
            }

            bool isDataValid = _roleService.CheckValidation(roleDTO);

            if (!isDataValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("Name Can't be Empty !");
                return BadRequest(result);
            }
            Role role = _roleService.AddRole(roleDTO);
            result.Results = _roleMapper.MapToRoleDTO(role);
            result.StatusCode = Ok().StatusCode;
            result.Messages.Add("Added successful");
            _roleService.SaveChanges();

            return Ok(result);
        }

        /// <summary>
        ///  edit role data in db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult EditRole([FromRoute] int id, [FromBody] RoleDTO roleDTO)
        {
            ResultDTO<RoleDTO> result = new ResultDTO<RoleDTO>();


            if (!ModelState.IsValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("please add a valid role !");
                return BadRequest(result);
            }

            List<String> errors = _roleService.CheckEditValidation(id, roleDTO);

            if (errors.Count > 0)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages = errors;
                return BadRequest(result);
            }
            bool isEdited = _roleService.EditRole(id, roleDTO);
            if (!isEdited)
            {
                result.ErrorsMessages.Add("Role Not Found!!!!!!");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);

            }

            result.Results = roleDTO;
            result.StatusCode = Ok().StatusCode;
            result.Messages.Add("Role Updated successfully");
            _roleService.SaveChanges();
            return Ok(result);
        }

        /// <summary>
        /// delete role from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteRole([FromRoute] int id)
        {
            ResultDTO<RoleDTO> result = new ResultDTO<RoleDTO>();
            bool isRoleDeleted = _roleService.DeleteRole(id);
            if (isRoleDeleted)
            {

                result.StatusCode = Ok().StatusCode;
                result.Messages.Add("Deleted successful");
                _roleService.SaveChanges();
                return Ok(result);
            }
            else
            {
                result.StatusCode = NotFound().StatusCode;
                result.ErrorsMessages.Add("role not found !");

                return NotFound(result);
            }
        }

    }
}

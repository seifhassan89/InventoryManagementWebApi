using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contacts;

namespace Controllers
{
    [Route("MeasuringUnits")]
    [ApiController]
    public class MeasuringUnitController : Controller
    {
        private readonly IMeasuringUnitService _measuringUnitService;

        public MeasuringUnitController(IMeasuringUnitService measuringUnitService)
        {
            _measuringUnitService = measuringUnitService;
        }
        /// <summary>
        /// get all measuring units
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            ResultDTO<List<MeasuringUnitDTO>> result = new ResultDTO<List<MeasuringUnitDTO>>();
            result.Results = _measuringUnitService.GetAll();
            result.StatusCode = Ok().StatusCode;
            return Ok(result);
        }
        /// <summary>
        /// get measuring unit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            ResultDTO<MeasuringUnitDTO> result = new ResultDTO<MeasuringUnitDTO>();
            MeasuringUnitDTO? foundMeasure = _measuringUnitService.GetById(id);
            if (foundMeasure != null)
            {
                result.Results = foundMeasure;
                result.StatusCode = Ok().StatusCode;
                return Ok(result);
            }
            else
            {
                result.ErrorsMessages.Add("Measuring Unit Not Found !");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);
            }
        }
        /// <summary>
        /// delete measuring unit by id from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteMeasuringUnit([FromRoute] int id)
        {
            ResultDTO<MeasuringUnitDTO> result = new ResultDTO<MeasuringUnitDTO>();
            bool isDeleted = _measuringUnitService.DeleteMeasuringUnit(id);
            if (isDeleted == false)
            {
                result.ErrorsMessages.Add("Measuring Unit not found!");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);
            }
            else
            {
                result.Messages.Add("Deleted Succefully !");
                result.StatusCode = Ok().StatusCode;
                _measuringUnitService.SaveChanges();
                return Ok(result);
            }

        }
        /// <summary>
        /// edit mesuaring unit Data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="measuringUnitDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult EditMeasuringUnit([FromRoute] int id, [FromBody] MeasuringUnitDTO measuringUnitDTO)
        {

            ResultDTO<MeasuringUnitDTO> result = new ResultDTO<MeasuringUnitDTO>();

            if (!ModelState.IsValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("please add a valid measuringUnit !");
                return BadRequest(result);
            }
            List<String> errors = _measuringUnitService.CheckEditValidation(id, measuringUnitDTO);

            if (errors.Count > 0)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages = errors;
                return BadRequest(result);
            }
            bool isEdited = _measuringUnitService.EditMeasuringUnit(id, measuringUnitDTO);
            if (!isEdited)
            {
                result.ErrorsMessages.Add("Measuring Unit Not Found!!!!!!");
                result.StatusCode = NotFound().StatusCode;
                return NotFound(result);

            }

            result.Results = measuringUnitDTO;
            result.StatusCode = Ok().StatusCode;
            result.Messages.Add("Measuring Unit Updated successfully");
            _measuringUnitService.SaveChanges();
            return Ok(result);
        }
        /// <summary>
        /// add measuring unit to Db
        /// </summary>
        /// <param name="measuringUnitDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMeasuringUnit([FromBody] MeasuringUnitDTO measuringUnitDTO)
        {
            ResultDTO<MeasuringUnitDTO> result = new ResultDTO<MeasuringUnitDTO>();
            if (!ModelState.IsValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("please add a valid measuringUnit !");
                return BadRequest(result);
            }

            bool isDataValid = _measuringUnitService.CheckValidation(measuringUnitDTO);

            if (!isDataValid)
            {
                result.StatusCode = BadRequest().StatusCode;
                result.ErrorsMessages.Add("Name Can't be Empty !");
                return BadRequest(result);
            }
            result.Results = _measuringUnitService.AddMeasuringUnit(measuringUnitDTO);
            result.StatusCode = Ok().StatusCode;
            result.Messages.Add("Added successful");
            _measuringUnitService.SaveChanges();
            return Ok(result);

        }
    }
}

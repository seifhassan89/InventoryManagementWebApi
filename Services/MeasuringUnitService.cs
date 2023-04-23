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
    public class MeasuringUnitService : IMeasuringUnitService
    {
        private readonly IMeasuringUnitMapper _measuringUnitMapper;
        private readonly IMeasuringUnitRepository _measuringUnitRepository;


        public MeasuringUnitService(IMeasuringUnitMapper measuringUnitMapper, IMeasuringUnitRepository measuringUnitRepository)
        {
            _measuringUnitMapper = measuringUnitMapper;
            _measuringUnitRepository = measuringUnitRepository;
        }
        /// <summary>
        /// add measuring unit to db
        /// </summary>
        /// <param name="measuringUnitDto"></param>
        /// <returns></returns>
        public MeasuringUnitDTO AddMeasuringUnit(MeasuringUnitDTO measuringUnitDto)
        {
            MeasuringUnit measuringUnit = _measuringUnitMapper.MapToMeasuringUnit(measuringUnitDto);
            MeasuringUnit AddedMeasuringUnit = _measuringUnitRepository.AddMeasuringUnit(measuringUnit);
            MeasuringUnitDTO addedMeasuringUnit = _measuringUnitMapper.MapToMeasuringUnitDTO(AddedMeasuringUnit);
            return addedMeasuringUnit;
        }
        /// <summary>
        /// delete measuring unit from db
        /// </summary>
        /// <param name="measuringUnitId"></param>
        /// <returns></returns>
        public bool DeleteMeasuringUnit(int measuringUnitId)
        {
            bool deletedMeasuringUnit = _measuringUnitRepository.DeleteMeasuringUnit(measuringUnitId);
            return deletedMeasuringUnit;
        }
        /// <summary>
        /// edit measuring unit data
        /// </summary>
        /// <param name="measuringUnitId"></param>
        /// <param name="measuringUnitDTO"></param>
        /// <returns></returns>
        public bool EditMeasuringUnit(int measuringUnitId, MeasuringUnitDTO measuringUnitDTO)
        {
            MeasuringUnit measuringUnit = _measuringUnitMapper.MapToMeasuringUnit(measuringUnitDTO);
            return _measuringUnitRepository.EditMeasuringUnit(measuringUnitId, measuringUnit);

        }
        /// <summary>
        /// get all measuring units
        /// </summary>
        /// <returns></returns>
        public List<MeasuringUnitDTO> GetAll()
        {
            List<MeasuringUnitDTO> mappedList = new List<MeasuringUnitDTO>();

            List<MeasuringUnit> listOfMeasuringUnit = _measuringUnitRepository.GetAll();

            listOfMeasuringUnit.ForEach(measuringUnit =>
            {
                MeasuringUnitDTO measuringUnitDTO = _measuringUnitMapper.MapToMeasuringUnitDTO(measuringUnit);
                mappedList.Add(measuringUnitDTO);
            });

            return mappedList;
        }
        /// <summary>
        /// get measuring unit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MeasuringUnitDTO? GetById(int id)
        {
            MeasuringUnit? measuringUnit = _measuringUnitRepository.GetById(id);
            if (measuringUnit == null)
            {
                return null;
            }
            else
            {
                MeasuringUnitDTO mappedMeasuringUnit = _measuringUnitMapper.MapToMeasuringUnitDTO(measuringUnit);
                return mappedMeasuringUnit;
            }
        }

        public void SaveChanges()
        {
            _measuringUnitRepository.SaveChanges();
        }

        /// <summary>
        /// validate add measuring Unit data
        /// </summary>
        /// <param name="measuringUnitDTO"></param>
        /// <returns></returns>
        public bool CheckValidation(MeasuringUnitDTO measuringUnitDTO)
        {
            if (String.IsNullOrEmpty(measuringUnitDTO.Name))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// validate edit measuring Unit data 
        /// </summary>
        /// <param name="MeasuringUnitIdForRoute"></param>
        /// <param name="measuringUnitDTO"></param>
        /// <returns></returns>
        public List<String> CheckEditValidation(int MeasuringUnitIdForRoute, MeasuringUnitDTO measuringUnitDTO)
        {
            List<String> errors = new List<String>();

            if (String.IsNullOrEmpty(measuringUnitDTO.Name))
            {
                errors.Add("Name Can't Be Empty!");
            }
            if (measuringUnitDTO.MeasuringUnitId == 0)
            {
                errors.Add("please send measuring Unit Id!");
            }
            if (MeasuringUnitIdForRoute != measuringUnitDTO.MeasuringUnitId)
            {
                errors.Add("measuring Unit Id is not Identical with Measuring Unit from route!");
            }

            return errors;
        }

    }
}

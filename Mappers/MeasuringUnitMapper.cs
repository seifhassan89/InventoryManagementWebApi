using Models;
using Mappers.Contracts;

namespace Mappers
{
    public class MeasuringUnitMapper : IMeasuringUnitMapper
    {
        public MeasuringUnit MapToMeasuringUnit(MeasuringUnitDTO measuringUnitDTO)
        {
            MeasuringUnit measuringUnit = new MeasuringUnit();

            measuringUnit.MeasuringUnitId = measuringUnitDTO.MeasuringUnitId;
            measuringUnit.Name = measuringUnitDTO.Name;

            return measuringUnit;
        }

        public MeasuringUnitDTO MapToMeasuringUnitDTO(MeasuringUnit measuringUnit)
        {
            MeasuringUnitDTO measuringUnitDTO = new MeasuringUnitDTO();

            measuringUnitDTO.MeasuringUnitId = measuringUnit.MeasuringUnitId;
            measuringUnitDTO.Name = measuringUnit.Name;

            return measuringUnitDTO;
        }

    }
}
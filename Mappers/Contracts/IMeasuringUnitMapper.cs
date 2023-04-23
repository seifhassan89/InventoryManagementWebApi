using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.Contracts
{
    public interface IMeasuringUnitMapper
    {
        MeasuringUnit MapToMeasuringUnit(MeasuringUnitDTO measuringUnitDTO);

        MeasuringUnitDTO MapToMeasuringUnitDTO(MeasuringUnit measuringUnit);
    }
}

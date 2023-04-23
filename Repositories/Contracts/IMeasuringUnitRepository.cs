using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IMeasuringUnitRepository : IBaseRepository
    {
        List<MeasuringUnit> GetAll();

        MeasuringUnit? GetById(int measuringUnitId);

        bool DeleteMeasuringUnit(int measuringUnitId);

        MeasuringUnit AddMeasuringUnit(MeasuringUnit measuringUnit);

        bool EditMeasuringUnit(int measuringUnitId, MeasuringUnit measuringUnit);

    }
}

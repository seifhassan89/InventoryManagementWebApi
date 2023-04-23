using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IMeasuringUnitService
    {
        public List<MeasuringUnitDTO> GetAll();
        public MeasuringUnitDTO? GetById(int id);
        public MeasuringUnitDTO AddMeasuringUnit(MeasuringUnitDTO measuringUnitDto);
        public bool EditMeasuringUnit(int measuringUnitId, MeasuringUnitDTO measuringUnitDTO);
        public bool DeleteMeasuringUnit(int measuringUnitId);
        public void SaveChanges();
        public bool CheckValidation(MeasuringUnitDTO measuringUnitDTO);
        public List<String> CheckEditValidation(int MeasuringUnitIdForRoute, MeasuringUnitDTO measuringUnitDTO);

    }
}

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IRequestTypeService
    {
        public List<RequestTypeDTO> GetAll();
        public RequestTypeDTO? GetById(int id);
        public RequestTypeDTO AddRequestType(RequestTypeDTO requestTypeDto);
        public bool EditRequestType(int requestTypeId, RequestTypeDTO requestTypeDTO);
        public bool DeleteRequestType(int requestTypeId);
        public void SaveChanges();

        public bool CheckValidation(RequestTypeDTO requestTypeDTO);

        public List<String> CheckEditValidation(int requestIdFrmRoute, RequestTypeDTO requestTypeDTO);


    }
}

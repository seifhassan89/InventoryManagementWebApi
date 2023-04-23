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
    public class RequestTypeService : IRequestTypeService
    {
        private readonly IRequestTypeMapper _requestTypeMapper;
        private readonly IRequestTypeRepository _requestTypeRepository;


        public RequestTypeService(IRequestTypeMapper requestTypeMapper, IRequestTypeRepository requestTypeRepository)
        {
            _requestTypeMapper = requestTypeMapper;
            _requestTypeRepository = requestTypeRepository;
        }
        /// <summary>
        /// add request type to db
        /// </summary>
        /// <param name="requestTypeDTO"></param>
        /// <returns></returns>
        public RequestTypeDTO AddRequestType(RequestTypeDTO requestTypeDTO)
        {
            RequestType requestType = _requestTypeMapper.MapToRequestType(requestTypeDTO);
            RequestType AddedRequestType = _requestTypeRepository.AddRequestType(requestType);
            RequestTypeDTO addedRequestType = _requestTypeMapper.MapToRequestTypeDTO(AddedRequestType);
            return addedRequestType;
        }
        /// <summary>
        /// delete request type from db
        /// </summary>
        /// <param name="requestTypeId"></param>
        /// <returns></returns>
        public bool DeleteRequestType(int requestTypeId)
        {
            bool isDeleted = _requestTypeRepository.DeleteRequestType(requestTypeId);
            return isDeleted;
        }
        /// <summary>
        /// edit request type data
        /// </summary>
        /// <param name="requestTypeId"></param>
        /// <param name="requestTypeDTO"></param>
        /// <returns></returns>
        public bool EditRequestType(int requestTypeId, RequestTypeDTO requestTypeDTO)
        {
            RequestType requestType = _requestTypeMapper.MapToRequestType(requestTypeDTO);
            return _requestTypeRepository.EditRequestType(requestTypeId, requestType);

        }
        /// <summary>
        /// get all request types from db
        /// </summary>
        /// <returns></returns>
        public List<RequestTypeDTO> GetAll()
        {
            List<RequestTypeDTO> mappedList = new List<RequestTypeDTO>();

            List<RequestType> listOfItem = _requestTypeRepository.GetAll();

            listOfItem.ForEach(requestType =>
            {
                RequestTypeDTO requestTypeDTO = _requestTypeMapper.MapToRequestTypeDTO(requestType);
                mappedList.Add(requestTypeDTO);
            });

            return mappedList;
        }
        /// <summary>
        /// get request type DTO from db by id                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RequestTypeDTO? GetById(int id)
        {
            RequestType? requestType = _requestTypeRepository.GetById(id);
            if (requestType == null)
            {
                return null;
            }
            else
            {

                RequestTypeDTO mappedRequest = _requestTypeMapper.MapToRequestTypeDTO(requestType);

                return mappedRequest;
            }
        }
        public void SaveChanges()
        {
            _requestTypeRepository.SaveChanges();
        }
        /// <summary>
        /// validate add request type data
        /// </summary>
        /// <param name="requestTypeDTO"></param>
        /// <returns></returns>
        public bool CheckValidation(RequestTypeDTO requestTypeDTO)
        {
            if (String.IsNullOrEmpty(requestTypeDTO.Name))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// validate edit request type data
        /// </summary>
        /// <param name="requestIdFrmRoute"></param>
        /// <param name="requestTypeDTO"></param>
        /// <returns></returns>
        public List<String> CheckEditValidation(int requestIdFrmRoute, RequestTypeDTO requestTypeDTO)
        {
            List<String> errors = new List<String>();

            if (String.IsNullOrEmpty(requestTypeDTO.Name))
            {
                errors.Add("Name Can't Be Empty!");
            }
            if (requestTypeDTO.RequestTypeId == 0)
            {
                errors.Add("please send RequestTypeId!");
            }
            if (requestIdFrmRoute != requestTypeDTO.RequestTypeId)
            {
                errors.Add("RequestTypeId is not Identical with Request Type id from route!");
            }

            return errors;
        }

    }
}

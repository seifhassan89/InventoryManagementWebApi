using employee_task.Models;
using Newtonsoft.Json;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserMapper _userMapper;
        public UserService(IUserMapper userMapper, IUserRepository userRepository)
        {

            _userMapper = userMapper;
            _userRepository = userRepository;
        }
        /// <summary>
        /// add user to Db
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public UserDTO AddUser(UserDTO userDTO)
        {
            User user = _userMapper.MapToUser(userDTO);
            User addedUser = _userRepository.AddUser(user);
            UserDTO returnedUserDTO = _userMapper.MapToUserDTO(addedUser);
            return returnedUserDTO;
        }
        /// <summary>
        /// delete user from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(int id)
        {
            User? user = _userRepository.GetById(id);
            if (user == null)
            {
                return false;
            }
            _userRepository.DeleteUser(user);
            return true;

        }
        /// <summary>
        /// edit user data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public bool EditUser(int userId, UserDTO userDTO)
        {
            User user = _userMapper.MapToUser(userDTO);
            return _userRepository.EditUser(userId, user);
        }
        /// <summary>
        /// get all users list
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> GetAll()
        {
            List<UserDTO> list = new List<UserDTO>();

            List<User> listOfUser = _userRepository.GetAll();

            listOfUser.ForEach(user =>
            {

                UserDTO userDTO = _userMapper.MapToUserDTO(user);
                list.Add(userDTO);

            });

            return list;

        }

        /// <summary>
        /// get users list paginated and filtered
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="FilterObject"></param>
        /// <returns></returns>
        public PagedResultDTO<UserDTO> GetAllPaged(int currentPage, int pageSize, string? FilterObject)
        {
            PagedResultDTO<UserDTO> pagedResultDTO = new PagedResultDTO<UserDTO>();

            UserDTO JsonObj = new UserDTO();

            if (!String.IsNullOrEmpty(FilterObject))
            {
                JsonObj = JsonConvert.DeserializeObject<UserDTO>(FilterObject);
            }

            List<UserDTO> list = new List<UserDTO>();

            List<User> listOfUser = _userRepository.GetAllPaged(pageSize, currentPage, JsonObj);

            listOfUser.ForEach(user =>
            {

                UserDTO userDTO = _userMapper.MapToUserDTO(user);
                list.Add(userDTO);

            });
            pagedResultDTO.List = list;
            pagedResultDTO.PageNumber = currentPage;
            pagedResultDTO.TotalRecords = _userRepository.GetAllPagedCount(JsonObj);

            return pagedResultDTO;

        }

        /// <summary>
        /// get user DTO by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO? GetById(int id)
        {
            User? user = _userRepository.GetById(id);
            if (user == null)
            {
                return null;
            }
            else
            {
                UserDTO userDTO = _userMapper.MapToUserDTO(user);

                return userDTO;
            }

        }

        public void SaveChanges()
        {
            _userRepository.SaveChanges();
        }
        /// <summary>
        /// check if user data is valid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public List<String> GetValidation(int? id, UserDTO userDTO)
        {
            List<String> errors = new List<String>();

            if (String.IsNullOrEmpty(userDTO.Name))
            {
                errors.Add("Please Add User Name");
            }
            if (String.IsNullOrEmpty(userDTO.Email))
            {
                errors.Add("Please Add User Email");
            }

            if (String.IsNullOrEmpty(userDTO.Fax))
            {
                errors.Add("Please Add User Fax");
            }

            if (String.IsNullOrEmpty(userDTO.Phone))
            {
                errors.Add("Please Add User Phone");
            }

            if (String.IsNullOrEmpty(userDTO.Website))
            {
                errors.Add("Please Add User Website");
            }

            if (id != null)
            {
                if (userDTO.UserId == 0)
                {
                    errors.Add("please send UserId");
                }

                if (userDTO.UserId != id)
                {
                    errors.Add("UserId Should be intentical with id in route");
                }
            }
            return errors;
        }

    }
}

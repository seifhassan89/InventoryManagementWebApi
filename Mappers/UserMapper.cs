using Models;
using Mappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers
{
    public class UserMapper : IUserMapper
    {
        public User MapToUser(UserDTO userDTO)
        {
            User user = new User();
            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Fax = userDTO.Fax;
            user.Website = userDTO.Website;
            user.Phone = userDTO.Phone;
            user.UserId = userDTO.UserId;

            return user;
        }

        public UserDTO MapToUserDTO(User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Name = user.Name;
            userDTO.Email = user.Email;
            userDTO.Fax = user.Fax;
            userDTO.Website = user.Website;
            userDTO.Phone = user.Phone;
            userDTO.UserId = user.UserId;


            return userDTO;
        }
    }
}

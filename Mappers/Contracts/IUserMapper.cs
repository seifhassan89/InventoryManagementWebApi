using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers.Contracts
{
    public interface IUserMapper
    {
        User MapToUser(UserDTO userDTO);

        UserDTO MapToUserDTO(User user);

    }
}

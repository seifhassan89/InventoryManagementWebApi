using employee_task.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IUserService
    {
        List<UserDTO> GetAll();
        PagedResultDTO<UserDTO> GetAllPaged(int currentPage, int pageSize, string? FilterObject);
        UserDTO? GetById(int id);
        UserDTO AddUser(UserDTO userDTO);
        bool EditUser(int userId, UserDTO userDTO);
        bool DeleteUser(int id);

        void SaveChanges();

        List<String> GetValidation(int? id, UserDTO userDTO);

    }
}

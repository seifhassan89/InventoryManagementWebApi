using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IUserRepository : IBaseRepository
    {
        List<User> GetAll();
        List<User> GetAllPaged(int pageSize, int currentPage, UserDTO FilterdObj);
        int GetAllPagedCount(UserDTO FilterdObj);

        User? GetById(int userId);

        void DeleteUser(User user);

        User AddUser(User user);

        bool EditUser(int userId, User user);

    }
}

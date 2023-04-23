using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Data;
using Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private StocksDB _StoresDB;

        public UserRepository(StocksDB StoresDB) : base(StoresDB)
        {

            _StoresDB = StoresDB;
        }
        /// <summary>
        /// add user to DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User AddUser(User user)
        {
            EntityEntry<User> addeduser = _StoresDB.Users.Add(user);

            return addeduser.Entity;
        }
        /// <summary>
        /// delete user from database
        /// </summary>
        /// <param name="foundUser"></param>
        public void DeleteUser(User foundUser)
        {
            _StoresDB.Users.Remove(foundUser);
        }

        /// <summary>
        /// edit user data 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool EditUser(int userId, User user)
        {
            User? foundUser = _StoresDB.Users.FirstOrDefault(u => u.UserId == userId);
            if (foundUser == null)
            {
                return false;
            }
            else
            {

                _StoresDB.Entry(foundUser).State = EntityState.Detached;
            }

            _StoresDB.Attach(user);
            _StoresDB.Entry(user).State = EntityState.Modified;

            return true;
        }

        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            List<User> listOfuser = _StoresDB.Users.ToList();
            return listOfuser;
        }

        /// <summary>
        /// get users list paginated and filtered
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="FilterdObj"></param>
        /// <returns></returns>
        public List<User> GetAllPaged(int pageSize, int currentPage, UserDTO FilterdObj)
        {
            int skiip = (currentPage - 1) * pageSize;

            List<User> listOfuser = _StoresDB.Users.Where(u =>
            (String.IsNullOrEmpty(FilterdObj.NameOrEmail) ||
            u.Name.ToLower().Contains(FilterdObj.NameOrEmail.ToLower()) ||
            u.Email.ToLower().Contains(FilterdObj.NameOrEmail.ToLower()))
            ).Skip(skiip).Take(pageSize).ToList();



            return listOfuser;
        }

        /// <summary>
        /// all users count
        /// </summary>
        /// <param name="FilterdObj"></param>
        /// <returns></returns>
        public int GetAllPagedCount(UserDTO FilterdObj)
        {
            int totalCount = _StoresDB.Users.Where(u =>
            (String.IsNullOrEmpty(FilterdObj.NameOrEmail) ||
            u.Name.ToLower().Contains(FilterdObj.NameOrEmail.ToLower()) ||
            u.Email.ToLower().Contains(FilterdObj.NameOrEmail.ToLower()))
            ).Count();

            return totalCount;

        }

        /// <summary>
        /// get user entity by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User? GetById(int userId)
        {
            User? foundedUser = _StoresDB.Users.FirstOrDefault(u => u.UserId == userId);
            return foundedUser;

        }
    }
}

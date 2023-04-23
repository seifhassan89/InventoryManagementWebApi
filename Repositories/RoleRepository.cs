using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Repositories.Contracts;

namespace Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        private StocksDB _StoresDB;
        public RoleRepository(StocksDB StoresDB) : base(StoresDB)
        {

            _StoresDB = StoresDB;
        }

        /// <summary>
        /// get all roles 
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAll()
        {
            List<Role> roles = (from role in _StoresDB.Roles select role).ToList();
            return roles;
        }

        /// <summary>
        /// get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetById(int id)
        {
            Role role = new Role();
            role = (from g in _StoresDB.Roles where g.RoleId == id select g).FirstOrDefault();
            return role;
        }

        /// <summary>
        /// delete role
        /// </summary>
        /// <param name="role"></param>
        public void DeleteRole(Role role)
        {
            _StoresDB.Remove(role);
        }

        /// <summary>
        /// add role to database
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Role AddRole(Role role)
        {
            EntityEntry<Role> x = _StoresDB.Roles.Add(role);
            return x.Entity;

        }

        /// <summary>
        /// edit role data
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="role"></param>
        public bool EditRole(int roleId, Role role)
        {
            Role? existingEntity = _StoresDB.Roles.Find(roleId);
            if (existingEntity == null)
            {
                return false;
            }
            else
            {
                _StoresDB.Entry(existingEntity).State = EntityState.Detached;
            }
            _StoresDB.Attach(role);
            _StoresDB.Entry(role).State = EntityState.Modified;
            return true;

        }
    }
}
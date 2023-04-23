using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRoleRepository : IBaseRepository
    {
        public List<Role> GetAll();

        public Role? GetById(int id);

        public Role AddRole(Role role);

        bool EditRole(int roleId, Role role);

        public void DeleteRole(Role role);

    }
}

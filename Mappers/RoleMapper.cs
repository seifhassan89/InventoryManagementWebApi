using Models;
using Mappers.Contracts;

namespace Mappers
{
    public class RoleMapper : IRoleMapper
    {
        public Role MapToRole(RoleDTO roleDTO)
        {
            Role role = new Role();
            role.Name = roleDTO.Name;
            role.RoleId = roleDTO.RoleId;
            return role;
        }

        public RoleDTO MapToRoleDTO(Role role)
        {
            RoleDTO roleDTO = new RoleDTO();
            roleDTO.Name = role.Name;
            roleDTO.RoleId = role.RoleId;
            return roleDTO;
        }
    }
}
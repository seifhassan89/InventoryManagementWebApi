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
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMapper _roleMapper;

        public RoleService(IRoleMapper roleMapper, IRoleRepository roleRepository)
        {
            _roleMapper = roleMapper;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// get list of roles DTO
        /// </summary>
        /// <returns></returns>
        public List<RoleDTO> GetAll()
        {
            List<RoleDTO> roles = new List<RoleDTO>();
            roles = _roleRepository.GetAll().Select(x => _roleMapper.MapToRoleDTO(x)).ToList();
            return roles;
        }

        /// <summary>
        /// get role DTO by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleDTO? GetById(int id)
        {
            Role? role = _roleRepository.GetById(id);
            if (role == null)
            {
                return null;
            }
            RoleDTO roleDTO = _roleMapper.MapToRoleDTO(role);
            return roleDTO;
        }

        /// <summary>
        /// add role to db
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Role AddRole(RoleDTO role)
        {
            Role roleEntity = _roleMapper.MapToRole(role);
            return _roleRepository.AddRole(roleEntity);
        }

        /// <summary>
        /// delete role 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteRole(int id)
        {
            Role? role = _roleRepository.GetById(id); ;
            if (role == null)
            {
                return false;
            }
            _roleRepository.DeleteRole(role);
            return true;

        }

        /// <summary>
        /// edit role data 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public bool EditRole(int roleId, RoleDTO roleDTO)
        {
            Role roleEntity = _roleMapper.MapToRole(roleDTO);
            return _roleRepository.EditRole(roleId, roleEntity);
        }

        /// <summary>
        /// save changes to DB
        /// </summary>
        public void SaveChanges()
        {
            _roleRepository.SaveChanges();
        }

        /// <summary>
        /// validate add role data
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public bool CheckValidation(RoleDTO roleDTO)
        {
            if (String.IsNullOrEmpty(roleDTO.Name))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// validate edit role data 
        /// </summary>
        /// <param name="RoleIdForRoute"></param>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public List<String> CheckEditValidation(int RoleIdForRoute, RoleDTO roleDTO)
        {
            List<String> errors = new List<String>();

            if (String.IsNullOrEmpty(roleDTO.Name))
            {
                errors.Add("Name Can't Be Empty!");
            }
            if (roleDTO.RoleId == 0)
            {
                errors.Add("please send roleId!");
            }
            if (RoleIdForRoute != roleDTO.RoleId)
            {
                errors.Add("roleId is not Identical with Role from route!");
            }

            return errors;
        }

    }
}

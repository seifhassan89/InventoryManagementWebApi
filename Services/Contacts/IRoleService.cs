using Models;

namespace Services.Contacts
{
    public interface IRoleService
    {
        public List<RoleDTO> GetAll();
        public RoleDTO? GetById(int id);
        public bool EditRole(int roleId, RoleDTO roleDTO);
        public bool DeleteRole(int id);
        public Role AddRole(RoleDTO role);
        public void SaveChanges();
        public bool CheckValidation(RoleDTO roleDTO);
        public List<String> CheckEditValidation(int RoleIdForRoute, RoleDTO roleDTO);
    }
}

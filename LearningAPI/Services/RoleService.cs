using WMS_ERP_Backend.DAOProject.DAO;
using WMS_ERP_Backend.DAOProject.IDAO;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.Services
{
    public class RoleService
    {
        private readonly IRoleDAO _roleDAO;

        public RoleService(IRoleDAO roleDAO)
        {
            _roleDAO = roleDAO;
        }

        public async Task<List<Role>> GetAll()
        {
            return await _roleDAO.GetAll();
        }

        public async Task<Role> GetById(int id)
        {
            return await _roleDAO.GetById(id);
        }

        public async Task Create(Role role)
        {
            await _roleDAO.Create(role);
        }

        public async Task Delete(int id)
        {
            await _roleDAO.Delete(id);
        }

        public async Task Update(Role role)
        {
            await _roleDAO.Update(role);
        }

        public async Task DeleteMany(List<int> rolesIds)
        {
            await _roleDAO.DeleteMany(rolesIds);
        }
    }
}

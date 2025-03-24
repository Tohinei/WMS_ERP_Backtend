using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DAOProject.IDAO
{
    public interface IRoleDAO
    {
        Task<List<Role>> GetAll();
        Task<Role> GetById(int id);
        Task Create(Role role);
        Task Update(Role role);
        Task Delete(int id);
        Task DeleteMany(List<int> roles);
    }
}

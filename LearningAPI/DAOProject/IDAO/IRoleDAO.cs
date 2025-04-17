using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DaoProject.IDao
{
    public interface IRoleDao
    {
        Role GetById(int roleId);
        List<Role> GetAll();
        int Create(Role role);
        bool Update(Role role);
        bool Delete(int roleId);
    }
}

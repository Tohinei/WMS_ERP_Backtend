using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DaoProject.IDao
{
    public interface IMenuDao
    {
        object GetById(int menuId);
        List<Menu> GetAll();
        int Create(Menu menu);
        bool Update(Menu menu);
        bool Delete(int menuId);
    }
}

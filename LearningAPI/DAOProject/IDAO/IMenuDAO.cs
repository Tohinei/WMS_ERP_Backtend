using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DAOProject.IDAO
{
    public interface IMenuDAO
    {
        Task<List<Menu>> GetAll();
        Task<Menu> GetById(int id);
        Task<int> Create(Menu menu);
        Task Update(Menu menu);
        Task Delete(int id);
        Task DeleteMany(List<int> Menus);
    }
}

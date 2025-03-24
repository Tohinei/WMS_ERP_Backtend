using WMS_ERP_Backend.DAOProject.IDAO;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.Services
{
    public class MenuService
    {
        private readonly IMenuDAO _menuDAO;

        public MenuService(IMenuDAO menuDAO)
        {
            _menuDAO = menuDAO;
        }

        public async Task<List<Menu>> GetAll()
        {
            return await _menuDAO.GetAll();
        }

        public async Task<Menu> GetById(int id)
        {
            return await _menuDAO.GetById(id);
        }

        public async Task<int> Create(Menu menu)
        {
            var id = await _menuDAO.Create(menu);
            return id;
        }

        public async Task Delete(int id)
        {
            await _menuDAO.Delete(id);
        }

        public async Task Update(Menu menu)
        {
            await _menuDAO.Update(menu);
        }

        public async Task DeleteMany(List<int> menusIds)
        {
            await _menuDAO.DeleteMany(menusIds);
        }
    }
}

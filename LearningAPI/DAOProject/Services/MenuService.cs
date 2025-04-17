using System.Collections.Generic;
using WMS_ERP_Backend.DaoProject.IDao;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.Services
{
    public class MenuService
    {
        private readonly IMenuDao _menuDao;

        public MenuService(IMenuDao menuDao)
        {
            _menuDao = menuDao;
        }

        public object GetById(int menuId)
        {
            return _menuDao.GetById(menuId);
        }

        public List<Menu> GetAll()
        {
            return _menuDao.GetAll();
        }

        public int Create(Menu menu)
        {
            return _menuDao.Create(menu);
        }

        public bool Update(Menu menu)
        {
            return _menuDao.Update(menu);
        }

        public bool Delete(int menuId)
        {
            return _menuDao.Delete(menuId);
        }
    }
}

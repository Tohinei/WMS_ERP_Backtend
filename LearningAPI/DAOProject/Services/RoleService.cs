using System.Collections.Generic;
using WMS_ERP_Backend.DaoProject.IDao;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.Services
{
    public class RoleService
    {
        private readonly IRoleDao _roleDao;

        public RoleService(IRoleDao roleDao)
        {
            _roleDao = roleDao;
        }

        public Role GetById(int roleId)
        {
            return _roleDao.GetById(roleId);
        }

        public List<Role> GetAll()
        {
            return _roleDao.GetAll();
        }

        public int Create(Role role)
        {
            return _roleDao.Create(role);
        }

        public bool Update(Role role)
        {
            return _roleDao.Update(role);
        }

        public bool Delete(int roleId)
        {
            return _roleDao.Delete(roleId);
        }
    }
}

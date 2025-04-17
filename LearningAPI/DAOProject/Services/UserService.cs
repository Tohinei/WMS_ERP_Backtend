using System.Collections.Generic;
using WMS_ERP_Backend.DaoProject.IDao;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.Services
{
    public class UserService
    {
        private readonly IUserDao _userDao;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public User GetById(int userId)
        {
            return _userDao.GetById(userId);
        }

        public List<User> GetAll()
        {
            return _userDao.GetAll();
        }

        public int Create(User user)
        {
            return _userDao.Create(user);
        }

        public bool Update(User user)
        {
            return _userDao.Update(user);
        }

        public bool Delete(int userId)
        {
            return _userDao.Delete(userId);
        }

        public bool DeleteMany(List<int> usersIds)
        {
            return _userDao.DeleteMany(usersIds);
        }
    }
}

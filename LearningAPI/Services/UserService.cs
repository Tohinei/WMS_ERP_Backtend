using WMS_ERP_Backend.DAOProject.IDAO;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.Services
{
    public class UserService
    {
        private readonly IUserDAO _userDAO;

        public UserService(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public async Task<List<User>> GetAll()
        {
            return await _userDAO.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            return await _userDAO.GetById(id);
        }

        public async Task Create(User user)
        {
            await _userDAO.Create(user);
        }

        public async Task Delete(int id)
        {
            await _userDAO.Delete(id);
        }

        public async Task Update(User user)
        {
            await _userDAO.Update(user);
        }

        public async Task DeleteMany(List<int> usersId)
        {
            await _userDAO.DeleteMany(usersId);
        }
    }
}

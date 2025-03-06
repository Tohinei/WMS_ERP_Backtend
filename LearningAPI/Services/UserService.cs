using LearningAPI.DAOProject.IDAO;
using LearningAPI.Models;

namespace LearningAPI.Services
{
    public class UserService
    {
        private readonly IUserDAO _userDAO;

        public UserService(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public async Task<List<User>> GetAll() => await _userDAO.GetAll();

        public async Task<User?> GetById(int id) => await _userDAO.GetById(id);

        public async Task<List<User>> GetByRole(String role) => await _userDAO.GetByRole(role);

        public async Task Add(User user) => await _userDAO.Add(user);

        public async Task Delete(int id) => await _userDAO.Delete(id);

        public async Task Update(User user) => await _userDAO.Update(user);

        public async Task UpdateRole(int id, String role) => await _userDAO.UpdateRole(id, role);

        public async Task DeleteUsers(int[] usersIds) => await _userDAO.DeleteUsers(usersIds);
    }
}

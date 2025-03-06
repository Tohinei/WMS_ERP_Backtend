using LearningAPI.Models;

namespace LearningAPI.DAOProject.IDAO
{
    public interface IUserDAO
    {
        Task<List<User>> GetAll();
        Task<User?> GetById(int id);

        Task<List<User>> GetByRole(string Role);
        Task Add(User user);
        Task Update(User user);
        Task UpdateRole(int id, string role);
        Task Delete(int id);
        Task DeleteUsers(int[] userIds);
    }
}

using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DAOProject.IDAO
{
    public interface IUserDAO
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task Create(User user);
        Task Update(User updatedUser);
        Task Delete(int id);
        Task DeleteMany(List<int> usersId);
    }
}

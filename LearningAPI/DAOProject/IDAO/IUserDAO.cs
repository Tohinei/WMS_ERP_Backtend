using System.Threading.Tasks;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DaoProject.IDao
{
    public interface IUserDao
    {
        User GetById(int userId);
        List<User> GetAll();
        int Create(User user);
        bool Update(User user);
        bool Delete(int userId);

        bool DeleteMany(List<int> usersIds);
    }
}

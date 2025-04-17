using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DaoProject.IDao
{
    public interface ISessionDao
    {
        Session GetById(int sessionId);
        List<Session> GetAll();
        int Create(Session session);
        bool Update(Session session);
        bool Delete(int sessionId);
    }
}

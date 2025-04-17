using System.Collections.Generic;
using WMS_ERP_Backend.DaoProject.IDao;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.Services
{
    public class SessionService
    {
        private readonly ISessionDao _sessionDao;

        public SessionService(ISessionDao sessionDao)
        {
            _sessionDao = sessionDao;
        }

        public Session GetById(int sessionId)
        {
            return _sessionDao.GetById(sessionId);
        }

        public List<Session> GetAll()
        {
            return _sessionDao.GetAll();
        }

        public int Create(Session session)
        {
            return _sessionDao.Create(session);
        }

        public bool Update(Session session)
        {
            return _sessionDao.Update(session);
        }

        public bool Delete(int sessionId)
        {
            return _sessionDao.Delete(sessionId);
        }
    }
}

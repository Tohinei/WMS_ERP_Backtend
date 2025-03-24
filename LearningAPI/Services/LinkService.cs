using WMS_ERP_Backend.DAOProject.IDAO;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.Services
{
    public class LinkService
    {
        private readonly ILinkDAO _linkDAO;

        public LinkService(ILinkDAO linkDAO)
        {
            _linkDAO = linkDAO;
        }

        public async Task<List<Link>> GetAll()
        {
            return await _linkDAO.GetAll();
        }

        public async Task<Link> GetById(int id)
        {
            return await _linkDAO.GetById(id);
        }

        public async Task Create(Link link)
        {
            await _linkDAO.Create(link);
        }

        public async Task Delete(int id)
        {
            await _linkDAO.Delete(id);
        }

        public async Task Update(Link link)
        {
            await _linkDAO.Update(link);
        }

        public async Task DeleteMany(List<int> links)
        {
            await _linkDAO.DeleteMany(links);
        }
    }
}

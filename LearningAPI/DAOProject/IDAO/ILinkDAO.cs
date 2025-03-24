using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DAOProject.IDAO
{
    public interface ILinkDAO
    {
        Task<List<Link>> GetAll();
        Task<Link> GetById(int id);
        Task Create(Link link);
        Task Update(Link link);
        Task Delete(int id);
        Task DeleteMany(List<int> links);
    }
}

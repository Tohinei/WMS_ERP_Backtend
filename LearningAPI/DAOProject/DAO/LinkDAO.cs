using Microsoft.EntityFrameworkCore;
using WMS_ERP_Backend.DAOProject.IDAO;
using WMS_ERP_Backend.Data;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DAOProject.DAO
{
    public class LinkDAO : ILinkDAO
    {
        private readonly AppDbContext _context;

        public LinkDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Link link)
        {
            await _context.Links.AddAsync(link);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Link? link = await _context.Links.FirstOrDefaultAsync(l => l.Id == id);
            if (link != null)
            {
                _context.Links.Remove(link);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMany(List<int> linksIds)
        {
            var links = await _context.Links.Where(l => linksIds.Contains(l.Id)).ToListAsync();

            if (links.Any())
            {
                _context.Links.RemoveRange(links);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Link>> GetAll()
        {
            return await _context.Links.ToListAsync();
        }

        public async Task<Link> GetById(int id)
        {
            Link? link = await _context.Links.FirstOrDefaultAsync(r => r.Id == id);
            if (link == null)
            {
                throw new ArgumentException("Link Not Found");
            }
            return link;
        }

        public async Task Update(Link updatedLink)
        {
            Link? link = await _context.Links.FirstOrDefaultAsync(l => l.Id == updatedLink.Id);
            if (link != null)
            {
                link.Name = updatedLink.Name;
                await _context.SaveChangesAsync();
            }
        }
    }
}

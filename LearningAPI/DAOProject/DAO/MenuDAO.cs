using Microsoft.EntityFrameworkCore;
using WMS_ERP_Backend.DAOProject.IDAO;
using WMS_ERP_Backend.Data;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DAOProject.DAO
{
    public class MenuDAO : IMenuDAO
    {
        private readonly AppDbContext _context;

        public MenuDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Menu menu)
        {
            var validLinks = new List<Link>();
            var linkAreValid = true;

            foreach (var link in menu.Links)
            {
                var exisitingLink = await _context.Links.FirstOrDefaultAsync(x => x.Id == link.Id);
                if (exisitingLink != null && linkAreValid)
                {
                    validLinks.Add(exisitingLink);
                    continue;
                }
                else
                {
                    linkAreValid = false;
                    break;
                }
            }
            menu.Links = validLinks;
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
            return menu.Id;
        }

        public async Task Delete(int id)
        {
            Menu? menu = await _context.Menus.FirstOrDefaultAsync(m => m.Id == id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMany(List<int> menuIds)
        {
            var menus = await _context.Links.Where(m => menuIds.Contains(m.Id)).ToListAsync();

            if (menus.Any())
            {
                _context.Links.RemoveRange(menus);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Menu>> GetAll()
        {
            return await _context.Menus.Include(m => m.Links).ToListAsync();
        }

        public async Task<Menu> GetById(int id)
        {
            Menu? menu = await _context
                .Menus.Include(m => m.Links)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                throw new ArgumentException("Menu Not Found");
            }
            return menu;
        }

        public async Task Update(Menu updatedMenu)
        {
            Menu? menu = await _context.Menus.FirstOrDefaultAsync(r => r.Id == updatedMenu.Id);
            if (menu != null)
            {
                menu.Name = updatedMenu.Name;
                menu.Links = updatedMenu.Links;
                await _context.SaveChangesAsync();
            }
        }
    }
}

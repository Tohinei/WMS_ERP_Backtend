using Microsoft.EntityFrameworkCore;
using WMS_ERP_Backend.DAOProject.IDAO;
using WMS_ERP_Backend.Data;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.DAOProject.DAO
{
    public class RoleDAO : IRoleDAO
    {
        private readonly AppDbContext _context;

        public RoleDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Role role)
        {
            var exisitingMenu = await _context.Menus.FirstOrDefaultAsync(x => x.Id == role.MenuId);
            if (exisitingMenu != null)
            {
                role.Menu = exisitingMenu;
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("menu doesn't exist");
            }
        }

        public async Task Delete(int id)
        {
            Role? role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMany(List<int> roleIds)
        {
            var roles = await _context.Links.Where(l => roleIds.Contains(l.Id)).ToListAsync();

            if (roles.Any())
            {
                _context.Links.RemoveRange(roles);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetById(int id)
        {
            Role? role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                throw new ArgumentException("Role Not Found");
            }
            return role;
        }

        public async Task Update(Role updatedRole)
        {
            Role? role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == updatedRole.Id);
            if (role != null)
            {
                role.Name = updatedRole.Name;
                role.Menu = updatedRole.Menu;
                await _context.SaveChangesAsync();
            }
        }
    }
}

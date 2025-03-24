using Microsoft.EntityFrameworkCore;
using WMS_ERP_Backend.Models;

namespace WMS_ERP_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}

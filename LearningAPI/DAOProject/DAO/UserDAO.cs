using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WMS_ERP_Backend.DAOProject.IDAO;
using WMS_ERP_Backend.Data;
using WMS_ERP_Backend.Models;
using WMS_ERP_Backend.Services;

namespace WMS_ERP_Backend.DAOProject.DAO
{
    public class UserDAO : IUserDAO
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public UserDAO(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = new JwtService("mysecretkeymysecretkeymysecretkeymysecretkeymysecretkey");
        }

        public async Task Create(User user)
        {
            var hashedPassword = _jwtService.HashPassword(user.Password);
            var existingRole = _context.Roles.FirstOrDefault(x => x.Id == user.RoleId);
            if (existingRole != null)
            {
                var newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Status = user.Status,
                    Email = user.Email,
                    Password = user.Password,
                    RoleId = user.RoleId,
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("role doesn't exist");
            }
        }

        public async Task Delete(int id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMany(List<int> userIds)
        {
            var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
            if (users != null)
            {
                _context.Users.RemoveRange(users);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAll()
        {
            return await _context
                .Users.Include(r => r.Role)
                .ThenInclude(m => m.Menu)
                .ThenInclude(l => l.Links)
                .ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            User? user = await _context
                .Users.Include(r => r.Role)
                .ThenInclude(m => m.Menu)
                .ThenInclude(l => l.Links)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException("User Not Found");
            }
            return user;
        }

        public async Task Update(User updatedUser)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.BirthDate = updatedUser.BirthDate;
                user.Email = updatedUser.Email;
                user.RoleId = updatedUser.RoleId;
                user.Role = updatedUser.Role;

                user.LastModified = DateTime.Now;

                user.Password = updatedUser.Password;

                //if (
                //    !string.IsNullOrEmpty(updatedUser.Password)
                //    && !_jwtService.VerifyPassword(updatedUser.Password, user.Password)
                //)
                //{
                //    user.Password = _jwtService.HashPassword(updatedUser.Password);
                //}
                //else { }

                await _context.SaveChangesAsync();
            }
        }
    }
}

using LearningAPI.DAOProject.IDAO;
using LearningAPI.Data;
using LearningAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LearningAPI.DAOProject.DAO
{
    public class UserDAO : IUserDAO
    {
        private readonly AppDbContext _context;

        public UserDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();

        public async Task<User?> GetById(int id) =>
            id == 0 ? null : await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            User? user = await GetById(id);
            if (user != null)
                _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetByRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                return new List<User>();

            return await _context.Users.Where(x => x.Role.ToString() == role).ToListAsync();
        }

        public async Task Update(User user)
        {
            if (user != null)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRole(int id, string role)
        {
            User? user = await GetById(id);
            if (user != null && !Enum.TryParse<Role>(role, true, out var parsedRole))
            {
                user.Role = parsedRole;
                await _context.SaveChangesAsync();
            }
        }

        [HttpDelete("/deleteUsers")]
        public async Task DeleteUsers(int[] userIds)
        {
            var filetredUsers = await _context
                .Users.Where(user => userIds.Contains(user.Id))
                .ToListAsync();

            if (filetredUsers.Any())
            {
                _context.Users.RemoveRange(filetredUsers);
                _context.SaveChanges();
            }
        }
    }
}

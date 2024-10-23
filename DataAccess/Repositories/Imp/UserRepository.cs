using Microsoft.EntityFrameworkCore;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Imp
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.UserImage).ToListAsync();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.UserImage)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserModel user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}

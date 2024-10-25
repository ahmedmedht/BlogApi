using Microsoft.EntityFrameworkCore;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Imp
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostModel>> GetAllAsync()
        {
            return await _context.Posts
                                 .Include(p => p.Category)
                                 .Include(p => p.PostSections)
                                 .Include(p => p.Comments)
                                 .Include(p => p.Reacts)
                                 .Include(p => p.Tags)
                                 .ToListAsync();
        }

        public async Task<PostModel> GetByIdAsync(Guid id)
        {
            return await _context.Posts
                                 .Include(p => p.Category)
                                 .Include(p => p.PostSections)
                                 .Include(p => p.Comments)
                                 .Include(p => p.Reacts)
                                 .Include(p => p.Tags)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(PostModel post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PostModel post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}

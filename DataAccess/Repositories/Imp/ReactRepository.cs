using Azure;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Imp
{
    public class ReactRepository : IReactRepository
    {
        private readonly ApplicationDbContext _context;

        public ReactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddReactionAsync(ReactModel reaction)
        {
            await _context.Reacts.AddAsync(reaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReactModel>> GetReactionsByPostIdAsync(Guid postId) =>
            await _context.Reacts.Where(r => r.PostId == postId).ToListAsync();

        public async Task<ReactModel> GetReactionByIdAsync(int reactionId) =>
            await _context.Reacts.FindAsync(reactionId);

        public async Task UpdateReactionAsync(ReactModel reaction)
        {
            _context.Reacts.Update(reaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReactionAsync(int reactionId)
        {
            var reaction = await _context.Reacts.FindAsync(reactionId);
            if (reaction != null)
            {
                _context.Reacts.Remove(reaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}

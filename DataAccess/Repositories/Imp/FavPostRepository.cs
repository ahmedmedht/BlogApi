using Microsoft.EntityFrameworkCore;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Imp
{
    public class FavPostRepository : IFavPostRepository
    {
        private readonly ApplicationDbContext _context;

        public FavPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFavoriteAsync(FavPostModel favorite)
        {
            await _context.FavPosts.AddAsync(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FavPostModel>> GetFavoritesByUserIdAsync(Guid userId) =>
            await _context.FavPosts.Where(f => f.UserId == userId).ToListAsync();

        public async Task<FavPostModel> GetFavoriteByIdAsync(int favoriteId) =>
            await _context.FavPosts.FindAsync(favoriteId);


        public async Task DeleteFavoriteAsync(int favoriteId)
        {
            var favorite = await _context.FavPosts.FindAsync(favoriteId);
            if (favorite != null)
            {
                _context.FavPosts.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }
    }
}

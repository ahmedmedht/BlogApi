using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IFavPostRepository
    {
        Task AddFavoriteAsync(FavPostModel favorite);
        Task<IEnumerable<FavPostModel>> GetFavoritesByPostIdAsync(Guid postId);
        Task<FavPostModel> GetFavoriteByIdAsync(int favoriteId);
        Task DeleteFavoriteAsync(int favoriteId);
    }
}

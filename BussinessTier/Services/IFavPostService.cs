using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IFavPostService
    {
        Task AddFavoriteAsync(FavDto dto);
        Task<IEnumerable<FavPostModel>> GetFavoritesByUserIdAsync(Guid userID);
        Task<FavPostModel> GetFavoriteByIdAsync(int favoriteId);
        Task DeleteFavoriteAsync(int favoriteId);
    }
}

using DataAccess.Repositories;
using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Imp
{
    public class FavPostService : IFavPostService
    {
        private readonly IFavPostRepository _favPostRepository;

        public FavPostService(IFavPostRepository favPostRepository)
        {
            _favPostRepository = favPostRepository;
        }

        public async Task AddFavoriteAsync(FavDto dto)
        {
            var favorite = new FavPostModel
            {
                PostId = dto.PostId,
                UserId =dto.UserId
            };
            await _favPostRepository.AddFavoriteAsync(favorite);
        }
        public async Task<IEnumerable<FavPostModel>> GetFavoritesByUserIdAsync(Guid userId) =>
            await _favPostRepository.GetFavoritesByUserIdAsync(userId);

        public async Task<FavPostModel> GetFavoriteByIdAsync(int favoriteId) =>
            await _favPostRepository.GetFavoriteByIdAsync(favoriteId);

        public async Task DeleteFavoriteAsync(int favoriteId) =>
            await _favPostRepository.DeleteFavoriteAsync(favoriteId);
    }
}

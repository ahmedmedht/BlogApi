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
    public class ReactService : IReactService
    {
        private readonly IReactRepository _reactRepository;

        public ReactService(IReactRepository reactRepository)
        {
            _reactRepository = reactRepository;
        }

        public async Task AddReactionAsync(ReactDTO dto)
        {
            var reaction = new ReactModel { 
                UserId = dto.UserId,
                PostId = dto.PostId,
                Liked =dto.Liked
            };
            await _reactRepository.AddReactionAsync(reaction);
        }
        public async Task<IEnumerable<ReactModel>> GetReactionsByPostIdAsync(Guid postId) =>
            await _reactRepository.GetReactionsByPostIdAsync(postId);

        public async Task<ReactModel> GetReactionByIdAsync(int reactionId) =>
            await _reactRepository.GetReactionByIdAsync(reactionId);

        public async Task UpdateReactionAsync(ReactModel reaction)
        {
            var oldReaction = await GetReactionByIdAsync(reaction.Id);
            oldReaction.Liked = reaction.Liked;
            await _reactRepository.UpdateReactionAsync(oldReaction);
        }
        public async Task DeleteReactionAsync(int reactionId) =>
            await _reactRepository.DeleteReactionAsync(reactionId);
    }
}

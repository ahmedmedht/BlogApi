using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IReactService
    {
        Task AddReactionAsync(ReactDTO dto);
        Task<IEnumerable<ReactModel>> GetReactionsByPostIdAsync(Guid postId);
        Task<ReactModel> GetReactionByIdAsync(int reactionId);
        Task UpdateReactionAsync(ReactModel reaction);
        Task DeleteReactionAsync(int reactionId);
    }
}

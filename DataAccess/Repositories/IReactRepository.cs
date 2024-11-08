using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IReactRepository
    {
        Task AddReactionAsync(ReactModel reaction);
        Task<IEnumerable<ReactModel>> GetReactionsByPostIdAsync(Guid postId);
        Task<ReactModel> GetReactionByIdAsync(int reactionId);
        Task UpdateReactionAsync(ReactModel reaction);
        Task DeleteReactionAsync(int reactionId);
    }
}

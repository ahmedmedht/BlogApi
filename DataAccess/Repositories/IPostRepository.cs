using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostModel>> GetAllAsync();
        Task<PostModel> GetByIdAsync(Guid id);
        Task AddAsync(PostModel post);
        Task UpdateAsync(PostModel post);
        Task DeleteAsync(Guid id);
    }
}

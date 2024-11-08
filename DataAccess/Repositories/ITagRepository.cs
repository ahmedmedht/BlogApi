using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<TagModel>> GetAllAsync();
        Task<TagModel> GetByIdAsync(int id);
        Task AddAsync(TagModel tag);
        Task UpdateAsync(TagModel tag);
        Task DeleteAsync(int id);
    }
}

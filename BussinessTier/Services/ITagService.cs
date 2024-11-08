using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagModel>> GetAllAsync();
        Task<TagModel> GetByIdAsync(int id);
        Task AddAsync(string tagName);
        Task UpdateAsync(TagDTO dto);
        Task DeleteAsync(int id);
    }
}

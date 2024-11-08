using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();
        Task<CategoryModel> GetByIdAsync(int id);
        Task AddAsync(string categoryName);
        Task UpdateAsync(CategoryDTO dto);
        Task DeleteAsync(int id);
    }
}

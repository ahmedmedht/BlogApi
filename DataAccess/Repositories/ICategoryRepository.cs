using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();
        Task<CategoryModel> GetByIdAsync(int id);
        Task AddAsync(CategoryModel category);
        Task UpdateAsync(CategoryModel category);
        Task DeleteAsync(int id);
    }
}

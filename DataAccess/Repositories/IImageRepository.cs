using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageModel>> GetAllAsync();
        Task<ImageModel> GetByIdAsync(Guid id);
        Task AddAsync(ImageModel image);
        Task UpdateAsync(ImageModel image);
        Task DeleteAsync(Guid id);
    }
}

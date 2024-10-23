using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(Guid id);
        Task AddAsync(UserModel user);
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(Guid id);
    }
}

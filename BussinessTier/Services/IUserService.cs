using Models.Dto;
using Models.Dto.ShowData;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDtoShow>> GetAllUsersAsync();
        Task<UserDtoShow> GetUserByIdAsync(Guid id);
        Task<UserModel> CreateUserAsync(UserDTO userDto);
        Task<UserModel> UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(Guid id);
    }
}

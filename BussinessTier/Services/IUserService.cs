using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<UserDTO> CreateUserAsync(UserDTO userDto);
        Task<UserDTO> UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(Guid id);
    }
}

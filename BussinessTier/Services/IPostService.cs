using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDTO>> GetAllPostsAsync();
        Task<PostDTO> GetPostByIdAsync(Guid id);
        Task CreatePostAsync(PostDTO postDto);
        Task UpdatePostAsync(PostDTO postDto);
        Task DeletePostAsync(Guid id);
    }
}

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
    public interface IPostService
    {
        Task<IEnumerable<PostShow>> GetAllPostsAsync();
        Task<PostShow> GetPostByIdAsync(Guid id);
        Task CreatePostAsync(PostDTO postDto);
        Task UpdatePostAsync(PostModel post);
        Task DeletePostAsync(Guid id);
    }
}

using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IPostSectionService
    {
        Task<IEnumerable<PostSectionDTO>> GetAllSectionsAsync(Guid postId);
        Task<PostSectionDTO> GetSectionByIdAsync(int sectionId);
        Task AddSectionAsync(PostSectionDTO sectionDto);
        Task UpdateSectionAsync(PostSectionDTO sectionDto);
        Task DeleteSectionAsync(int sectionId);
    }
}

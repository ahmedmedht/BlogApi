using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IPostSectionRepository
    {
        Task<IEnumerable<PostSectionModel>> GetAllSectionsAsync(Guid postId);
        Task<PostSectionModel> GetSectionByIdAsync(int sectionId);
        Task AddSectionAsync(PostSectionModel section);
        Task UpdateSectionAsync(PostSectionModel section);
        Task DeleteSectionAsync(int sectionId);
    }
}

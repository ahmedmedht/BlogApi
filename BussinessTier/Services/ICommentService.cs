using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ICommentService
    {
        Task AddCommentAsync(CommentDTO dto);
        Task<IEnumerable<CommentModel>> GetCommentsByPostIdAsync(Guid postId);
        Task<CommentModel> GetCommentByIdAsync(int commentId);
        Task UpdateCommentAsync(CommentModel comment);
        Task DeleteCommentAsync(int commentId);
    }
}

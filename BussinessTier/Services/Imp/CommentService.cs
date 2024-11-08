using DataAccess.Repositories;
using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Imp
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddCommentAsync(CommentDTO dto)
        {
            var comment = new CommentModel
            {
                UserId = dto.UserId,
                PostId = dto.PostId,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };
            await _commentRepository.AddCommentAsync(comment);
        }
        public async Task<IEnumerable<CommentModel>> GetCommentsByPostIdAsync(Guid postId) =>
            await _commentRepository.GetCommentsByPostIdAsync(postId);

        public async Task<CommentModel> GetCommentByIdAsync(int commentId) =>
            await _commentRepository.GetCommentByIdAsync(commentId);

        public async Task UpdateCommentAsync(CommentModel commentNew)
        {
            var comment = await GetCommentByIdAsync(commentNew.Id);
            comment.Comment = commentNew.Comment;
            await _commentRepository.UpdateCommentAsync(comment);
        }

        public async Task DeleteCommentAsync(int commentId) =>
            await _commentRepository.DeleteCommentAsync(commentId);
    }
}

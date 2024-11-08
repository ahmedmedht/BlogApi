using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Dto;
using Models.Model;
using System;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService commentService, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _logger = logger;
        }

        [HttpPost("AddNewComment")]
        public async Task<IActionResult> AddComment([FromBody] CommentDTO dto)
        {
            try
            {
                await _commentService.AddCommentAsync(dto);
                _logger.LogInformation("Comment added successfully to post ID {PostId}.", dto.PostId);
                return Ok("Comment added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment to post ID {PostId}.", dto.PostId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding comment.");
            }
        }

        [HttpGet("GetCommentsByPostId")]
        public async Task<IActionResult> GetCommentsByPost(Guid postId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByPostIdAsync(postId);
                _logger.LogInformation("Retrieved comments for post ID {PostId}.", postId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving comments for post ID {PostId}.", postId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving comments.");
            }
        }

        [HttpGet("GetCommetById")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(commentId);
                if (comment == null)
                {
                    _logger.LogWarning("Comment with ID {CommentId} not found.", commentId);
                    return NotFound();
                }
                _logger.LogInformation("Retrieved comment with ID {CommentId}.", commentId);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving comment with ID {CommentId}.", commentId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving comment.");
            }
        }

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment([FromBody] CommentModel comment)
        {
            try
            {
                await _commentService.UpdateCommentAsync(comment);
                _logger.LogInformation("Updated comment with ID {CommentId}.", comment.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating comment with ID {CommentId}.", comment.Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating comment.");
            }
        }

        [HttpDelete("DeleteCommentById")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                await _commentService.DeleteCommentAsync(commentId);
                _logger.LogInformation("Deleted comment with ID {CommentId}.", commentId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting comment with ID {CommentId}.", commentId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting comment.");
            }
        }
    }
}
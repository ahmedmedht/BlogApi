using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.Model;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostService postService, ILogger<PostController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        [HttpGet("GetAllPost")]
        public async Task<IActionResult> GetAllPosts()
        {
            _logger.LogInformation("Fetching all posts.");
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("GetPostById{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            _logger.LogInformation("Fetching post with ID: {PostId}", id);
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                _logger.LogWarning("Post with ID: {PostId} not found.", id);
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromForm] PostDTO postDto)
        {
            _logger.LogInformation("Creating a new post with title: {Title}", postDto.Title);
            await _postService.CreatePostAsync(postDto);
            _logger.LogInformation("Post created successfully.");
            return Ok();
        }

        [HttpPut("UpdatePost{id}")]
        public async Task<IActionResult> UpdatePost([FromForm] PostModel post)
        {
            _logger.LogInformation("Updating post with ID: {PostId}", post.Id);
            await _postService.UpdatePostAsync(post);
            _logger.LogInformation("Post updated successfully.");
            return Ok();
        }

        [HttpDelete("DeletePost{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            _logger.LogInformation("Deleting post with ID: {PostId}", id);
            await _postService.DeletePostAsync(id);
            _logger.LogInformation("Post deleted successfully.");
            return NoContent();
        }
    }
}

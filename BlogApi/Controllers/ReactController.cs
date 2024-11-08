using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.Model;
using Serilog;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        private readonly IReactService _reactService;
        private readonly ILogger<ReactController> _logger;

        public ReactController(IReactService reactService, ILogger<ReactController> logger)
        {
            _reactService = reactService;
            _logger = logger;
        }

        [HttpPost("AddReaction")]
        public async Task<IActionResult> AddReaction([FromBody] ReactDTO dto)
        {
            try
            {
                _logger.LogInformation("Attempting to add a reaction for PostId: {PostId}", dto.PostId);
                await _reactService.AddReactionAsync(dto);
                _logger.LogInformation("Reaction added successfully for PostId: {PostId}", dto.PostId);
                return Ok("Reaction added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding reaction for PostId: {PostId}", dto.PostId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetReactionsByPostId")]
        public async Task<IActionResult> GetReactionsByPost(Guid postId)
        {
            try
            {
                _logger.LogInformation("Fetching reactions for PostId: {PostId}", postId);
                var reactions = await _reactService.GetReactionsByPostIdAsync(postId);
                return Ok(reactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching reactions for PostId: {PostId}", postId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetReactionById")]
        public async Task<IActionResult> GetReactionById(int reactionId)
        {
            try
            {
                _logger.LogInformation("Fetching reaction with ReactionId: {ReactionId}", reactionId);
                var reaction = await _reactService.GetReactionByIdAsync(reactionId);
                return reaction == null ? NotFound() : Ok(reaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching reaction with ReactionId: {ReactionId}", reactionId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateReaction")]
        public async Task<IActionResult> UpdateReaction([FromBody] ReactModel reaction)
        {
            try
            {
                _logger.LogInformation("Updating reaction with ReactionId: {ReactionId}", reaction.Id);
                await _reactService.UpdateReactionAsync(reaction);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating reaction with ReactionId: {ReactionId}", reaction.Id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteReaction")]
        public async Task<IActionResult> DeleteReaction(int reactionId)
        {
            try
            {
                _logger.LogInformation("Deleting reaction with ReactionId: {ReactionId}", reactionId);
                await _reactService.DeleteReactionAsync(reactionId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting reaction with ReactionId: {ReactionId}", reactionId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Dto;
using System;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavPostController : ControllerBase
    {
        private readonly IFavPostService _favPostService;
        private readonly ILogger<FavPostController> _logger;

        public FavPostController(IFavPostService favPostService, ILogger<FavPostController> logger)
        {
            _favPostService = favPostService;
            _logger = logger;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddFavorite([FromBody] FavDto dto)
        {
            try
            {
                await _favPostService.AddFavoriteAsync(dto);
                _logger.LogInformation("Favorite added for Post ID {PostId} by User ID {UserId}.", dto.PostId, dto.UserId);
                return Ok("Favorite added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding favorite for Post ID {PostId} by User ID {UserId}.", dto.PostId, dto.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding favorite.");
            }
        }

        [HttpGet("Post/{postId}")]
        public async Task<IActionResult> GetFavoritesByPost(Guid postId)
        {
            try
            {
                var favorites = await _favPostService.GetFavoritesByPostIdAsync(postId);
                _logger.LogInformation("Retrieved favorites for Post ID {PostId}.", postId);
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorites for Post ID {PostId}.", postId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving favorites.");
            }
        }

        [HttpGet("{favoriteId}")]
        public async Task<IActionResult> GetFavoriteById(int favoriteId)
        {
            try
            {
                var favorite = await _favPostService.GetFavoriteByIdAsync(favoriteId);
                if (favorite == null)
                {
                    _logger.LogWarning("Favorite with ID {FavoriteId} not found.", favoriteId);
                    return NotFound();
                }
                _logger.LogInformation("Retrieved favorite with ID {FavoriteId}.", favoriteId);
                return Ok(favorite);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorite with ID {FavoriteId}.", favoriteId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving favorite.");
            }
        }

        [HttpDelete("{favoriteId}")]
        public async Task<IActionResult> DeleteFavorite(int favoriteId)
        {
            try
            {
                await _favPostService.DeleteFavoriteAsync(favoriteId);
                _logger.LogInformation("Deleted favorite with ID {FavoriteId}.", favoriteId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting favorite with ID {FavoriteId}.", favoriteId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting favorite.");
            }
        }
    }
}

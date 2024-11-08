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

        [HttpPost("AddPostToFav")]
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

        [HttpGet("GetFavByUserId")]
        public async Task<IActionResult> GetFavoritesByUserId(Guid userId)
        {
            try
            {
                var favorites = await _favPostService.GetFavoritesByUserIdAsync(userId);
                _logger.LogInformation("Retrieved favorites for User ID {UserId}.", userId);
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorites for User ID {UserId}.", userId);
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

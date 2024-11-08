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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                _logger.LogInformation("Retrieved all categories successfully.");
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all categories.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the server.");
            }
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found.", id);
                    return NotFound();
                }
                _logger.LogInformation("Retrieved category with ID {CategoryId}.", id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category with ID {CategoryId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the server.");
            }
        }

        [HttpPost("AddNewCategory")]
        public async Task<IActionResult> Add(string categoryName)
        {
            try
            {
                await _categoryService.AddAsync(categoryName);
                _logger.LogInformation("Added category: {CategoryName}.", categoryName);
                return Ok($"{categoryName} has been added to categories.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding category: {CategoryName}.", categoryName);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding category.");
            }
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> Update([FromForm] CategoryDTO dto)
        {
            try
            {
                await _categoryService.UpdateAsync(dto);
                _logger.LogInformation("Updated category with ID {CategoryId}.", dto.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category with ID {CategoryId}.", dto.Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating category.");
            }
        }

        [HttpDelete("DeleteCategoryById")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                _logger.LogInformation("Deleted category with ID {CategoryId}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID {CategoryId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting category.");
            }
        }
    }
}
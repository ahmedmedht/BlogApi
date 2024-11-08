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
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagService tagService, ILogger<TagController> logger)
        {
            _tagService = tagService;
            _logger = logger;
        }

        [HttpGet("GetAllTags")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tags = await _tagService.GetAllAsync();
                _logger.LogInformation("Retrieved all tags successfully.");
                return Ok(tags);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all tags.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the server.");
            }
        }

        [HttpGet("GetTagById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var tag = await _tagService.GetByIdAsync(id);
                if (tag == null)
                {
                    _logger.LogWarning("Tag with ID {TagId} not found.", id);
                    return NotFound();
                }
                _logger.LogInformation("Retrieved tag with ID {TagId}.", id);
                return Ok(tag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tag with ID {TagId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the server.");
            }
        }

        [HttpPost("AddNewTag")]
        public async Task<IActionResult> Add(string tagName)
        {
            try
            {
                await _tagService.AddAsync(tagName);
                _logger.LogInformation("Added tag: {TagName}.", tagName);
                return Ok($"{tagName} has been added to tags.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding tag: {TagName}.", tagName);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding tag.");
            }
        }

        [HttpPut("UpdateTag")]
        public async Task<IActionResult> Update([FromBody] TagDTO dto)
        {
            try
            {
                await _tagService.UpdateAsync(dto);
                _logger.LogInformation("Updated tag with ID {TagId}.", dto.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tag with ID {TagId}.", dto.Id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating tag.");
            }
        }

        [HttpDelete("DeleteTagById")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _tagService.DeleteAsync(id);
                _logger.LogInformation("Deleted tag with ID {TagId}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting tag with ID {TagId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting tag.");
            }
        }
    }
}

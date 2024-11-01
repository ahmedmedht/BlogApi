using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostSectionController : ControllerBase
    {
        private readonly IPostSectionService _postSectionService;
        private readonly ILogger<PostSectionController> _logger;

        public PostSectionController(IPostSectionService postSectionService, ILogger<PostSectionController> logger)
        {
            _postSectionService = postSectionService;
            _logger = logger;
        }

        [HttpGet("GetAllSectionForPost")]
        public async Task<IActionResult> GetAllSections(Guid postId)
        {
            _logger.LogInformation("Fetching all sections for post ID: {PostId}", postId);
            var sections = await _postSectionService.GetAllSectionsAsync(postId);
            return Ok(sections);
        }

        [HttpGet("GetSectionById{sectionId}")]
        public async Task<IActionResult> GetSectionById(int sectionId)
        {
            _logger.LogInformation("Fetching section with ID: {SectionId}", sectionId);
            var section = await _postSectionService.GetSectionByIdAsync(sectionId);
            if (section == null)
            {
                _logger.LogWarning("Section with ID: {SectionId} not found.", sectionId);
                return NotFound();
            }
            return Ok(section);
        }

        [HttpPost("AddSectionToPost")]
        public async Task<IActionResult> AddSection([FromBody] PostSectionDTO sectionDto)
        {
            _logger.LogInformation("Adding section to post ID: {PostId}", sectionDto.PostId);
            await _postSectionService.AddSectionAsync(sectionDto);
            _logger.LogInformation("Section added successfully.");
            return Ok();
        }

        [HttpPut("UpdateSection{sectionId}")]
        public async Task<IActionResult> UpdateSection([FromBody] PostSectionDTO sectionDto)
        {
            _logger.LogInformation("Updating section with ID: {SectionId}", sectionDto.Id);
            await _postSectionService.UpdateSectionAsync(sectionDto);
            _logger.LogInformation("Section updated successfully.");
            return NoContent();
        }

        [HttpDelete("DeleteSection{sectionId}")]
        public async Task<IActionResult> DeleteSection(Guid postId, int sectionId)
        {
            _logger.LogInformation("Deleting section with ID: {SectionId} from post ID: {PostId}", sectionId, postId);
            await _postSectionService.DeleteSectionAsync(sectionId);
            _logger.LogInformation("Section deleted successfully.");
            return NoContent();
        }
    }
}

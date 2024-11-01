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
    public class PostSectionService : IPostSectionService
    {
        private readonly IPostSectionRepository _postSectionRepository;

        public PostSectionService(IPostSectionRepository postSectionRepository)
        {
            _postSectionRepository = postSectionRepository;
        }

        public async Task<IEnumerable<PostSectionDTO>> GetAllSectionsAsync(Guid postId)
        {
            var sections = await _postSectionRepository.GetAllSectionsAsync(postId);
            return sections.Select(section => new PostSectionDTO
            {
                Id = section.Id,
                SectionText = section.SectionText,
                ImageId = section.ImageId,
                SectionOrder = section.SectionOrder
            }).ToList();
        }

        public async Task<PostSectionDTO> GetSectionByIdAsync(int sectionId)
        {
            var section = await _postSectionRepository.GetSectionByIdAsync(sectionId);
            if (section == null) return null;

            return new PostSectionDTO
            {
                Id = section.Id,
                SectionText = section.SectionText,
                ImageId = section.ImageId,
                SectionOrder = section.SectionOrder
            };
        }

        public async Task AddSectionAsync(PostSectionDTO sectionDto)
        {
            var section = new PostSectionModel
            {
                PostId = sectionDto.PostId,
                SectionText = sectionDto.SectionText,
                ImageId = sectionDto.ImageId,
                SectionOrder = sectionDto.SectionOrder
            };

            await _postSectionRepository.AddSectionAsync(section);
        }

        public async Task UpdateSectionAsync(PostSectionDTO sectionDto)
        {
            var section = await _postSectionRepository.GetSectionByIdAsync(sectionDto.Id);
            if (section == null) throw new Exception("Section not found.");

            section.SectionText = sectionDto.SectionText;
            section.ImageId = sectionDto.ImageId;
            section.SectionOrder = sectionDto.SectionOrder;

            await _postSectionRepository.UpdateSectionAsync(section);
        }

        public async Task DeleteSectionAsync(int sectionId)
        {
            await _postSectionRepository.DeleteSectionAsync(sectionId);
        }
    }
}

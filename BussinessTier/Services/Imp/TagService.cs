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
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<TagModel>> GetAllAsync() =>
            await _tagRepository.GetAllAsync();

        public async Task<TagModel> GetByIdAsync(int id) =>
            await _tagRepository.GetByIdAsync(id);

        public async Task AddAsync(string tagName)
        {
            var tag = new TagModel
            {
                Name = tagName
            };
            await _tagRepository.AddAsync(tag);
        }
        public async Task UpdateAsync(TagDTO dto)
        {
            var tag = await GetByIdAsync(dto.Id);
            tag.Name = dto.Name;
            await _tagRepository.UpdateAsync(tag);
        }
        public async Task DeleteAsync(int id) =>
            await _tagRepository.DeleteAsync(id);
    }
}

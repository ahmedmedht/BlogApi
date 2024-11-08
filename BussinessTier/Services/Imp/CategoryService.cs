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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync() =>
            await _categoryRepository.GetAllAsync();

        public async Task<CategoryModel> GetByIdAsync(int id) =>
            await _categoryRepository.GetByIdAsync(id);

        public async Task AddAsync(string categoryName){
            var category = new CategoryModel()
            {
                Name = categoryName,
            };
            await _categoryRepository.AddAsync(category); 
        }

        public async Task UpdateAsync(CategoryDTO dto)
        {
            var category = await GetByIdAsync(dto.Id);
            category.Name = dto.Name;
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id) =>
            await _categoryRepository.DeleteAsync(id);
    }
}

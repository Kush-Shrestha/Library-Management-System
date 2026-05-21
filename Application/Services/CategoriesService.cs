using LibraryCrud.Application.Repository;
using LibraryCrud.Domain.DTOs;
using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }                                         //If categoryRepository is null, it immediately throws an exception

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid category ID", nameof(id));

                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    throw new KeyNotFoundException($"Category with ID {id} not found");

                return MapToDto(category);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving category: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                return categories.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all categories: {ex.Message}", ex);
            }
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
        {
            try
            {
                if (categoryDto == null)
                    throw new ArgumentNullException(nameof(categoryDto));

                // Validate name uniqueness
                if (await _categoryRepository.NameExistsAsync(categoryDto.Name))
                    throw new InvalidOperationException($"Category with name '{categoryDto.Name}' already exists");

                var category = MapToEntity(categoryDto);
                var createdCategory = await _categoryRepository.AddAsync(category);
                return MapToDto(createdCategory);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating category: {ex.Message}", ex);
            }
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, CategoryDto categoryDto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid category ID", nameof(id));

                if (categoryDto == null)
                    throw new ArgumentNullException(nameof(categoryDto));

                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    throw new KeyNotFoundException($"Category with ID {id} not found");

                category.Name = categoryDto.Name;

                var updatedCategory = await _categoryRepository.UpdateAsync(category);
                return MapToDto(updatedCategory);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating category: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid category ID", nameof(id));

                return await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting category: {ex.Message}", ex);
            }
        }

        public async Task<CategoryDto> GetCategoryByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Category name cannot be empty", nameof(name));

                var category = await _categoryRepository.GetByNameAsync(name);
                if (category == null)
                    throw new KeyNotFoundException($"Category with name '{name}' not found");

                return MapToDto(category);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving category by name: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesWithBooksAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetCategoriesWithBooksAsync();
                return categories.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving categories with books: {ex.Message}", ex);
            }
        }

        private CategoryDto MapToDto(Categories category) => new()
        {
            ID = category.ID,
            Name = category.Name
        };

        private Categories MapToEntity(CategoryDto dto) => new()
        {
            ID = dto.ID,
            Name = dto.Name
        };
    }
}

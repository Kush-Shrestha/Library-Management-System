using LibraryCrud.Domain.DTOs;

namespace LibraryCrud.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto);
        Task<CategoryDto> UpdateCategoryAsync(int id, CategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<CategoryDto> GetCategoryByNameAsync(string name);
        Task<IEnumerable<CategoryDto>> GetCategoriesWithBooksAsync();
    }
}

using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Application.Repository
{
    public interface ICategoryRepository
    {
        Task<Categories> GetByIdAsync(int id);
        Task<IEnumerable<Categories>> GetAllAsync();
        Task<Categories> AddAsync(Categories category);
        Task<Categories> UpdateAsync(Categories category);
        Task<bool> DeleteAsync(int id);
        Task<Categories> GetByNameAsync(string name);
        Task<IEnumerable<Categories>> GetCategoriesWithBooksAsync();
        Task<bool> NameExistsAsync(string name);
    }
}

using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Application.Repository
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);
        Task<Book> GetByIsbnAsync(string isbn);
        Task<IEnumerable<Book>> GetBooksWithCategoryAsync();
        Task<bool> IsbnExistsAsync(string isbn);
    }
}

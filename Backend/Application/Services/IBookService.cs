using LibraryCrud.Domain.DTOs;
namespace LibraryCrud.Application.Services
{
    public interface IBookService
    {
        Task<BookDto> GetBookByIdAsync(int id);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> CreateBookAsync(BookDto bookDto);
        Task<BookDto> UpdateBookAsync(int id, BookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId);
        Task<BookDto> GetBookByIsbnAsync(string isbn);
        Task<IEnumerable<BookDto>> GetBooksWithCategoryAsync();
    }
}
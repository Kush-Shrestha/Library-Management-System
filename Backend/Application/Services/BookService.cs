using LibraryCrud.Application.Repository;
using LibraryCrud.Domain.DTOs;
using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid book ID", nameof(id));

            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");

            return MapToDto(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(MapToDto);
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto)
        {
            if (bookDto == null)
                throw new ArgumentNullException(nameof(bookDto));

            if (string.IsNullOrWhiteSpace(bookDto.ISBN))
                throw new ArgumentException("ISBN is required");

            if (await _bookRepository.IsbnExistsAsync(bookDto.ISBN))
                throw new InvalidOperationException($"Book with ISBN {bookDto.ISBN} already exists");

            var book = MapToEntity(bookDto);
            var createdBook = await _bookRepository.AddAsync(book);

            return MapToDto(createdBook);
        }

        public async Task<BookDto> UpdateBookAsync(int id, BookDto bookDto)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid book ID", nameof(id));

            if (bookDto == null)
                throw new ArgumentNullException(nameof(bookDto));

            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");

            book.Title = bookDto.Title ?? string.Empty;
            book.Author = bookDto.Author ?? string.Empty;
            book.ISBN = bookDto.ISBN ?? string.Empty;
            book.CategoryID = bookDto.CategoryID;

            var updatedBook = await _bookRepository.UpdateAsync(book);

            return MapToDto(updatedBook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid book ID", nameof(id));

            return await _bookRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID", nameof(categoryId));

            var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);
            return books.Select(MapToDto);
        }

        public async Task<BookDto> GetBookByIsbnAsync(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN cannot be empty", nameof(isbn));

            var book = await _bookRepository.GetByIsbnAsync(isbn);

            if (book == null)
                throw new KeyNotFoundException($"Book with ISBN {isbn} not found");

            return MapToDto(book);
        }

        public async Task<IEnumerable<BookDto>> GetBooksWithCategoryAsync()
        {
            var books = await _bookRepository.GetBooksWithCategoryAsync();
            return books.Select(MapToDto);
        }

        
        private BookDto MapToDto(Book book) => new()
        {
            ID = book.ID,
            Title = book.Title ?? string.Empty,
            Author = book.Author ?? string.Empty,
            ISBN = book.ISBN ?? string.Empty,
            CategoryID = book.CategoryID
        };

        private Book MapToEntity(BookDto dto) => new()
        {
            Title = dto.Title ?? string.Empty,
            Author = dto.Author ?? string.Empty,
            ISBN = dto.ISBN ?? string.Empty,
            CategoryID = dto.CategoryID,
            Category = null!
        };
    }
}
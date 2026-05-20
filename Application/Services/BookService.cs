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
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid book ID", nameof(id));

                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null)
                    throw new KeyNotFoundException($"Book with ID {id} not found");

                return MapToDto(book);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving book: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            try
            {
                var books = await _bookRepository.GetAllAsync();
                return books.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all books: {ex.Message}", ex);
            }
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto)
        {
            try
            {
                if (bookDto == null)
                    throw new ArgumentNullException(nameof(bookDto));

                // Validate ISBN uniqueness
                if (await _bookRepository.IsbnExistsAsync(bookDto.ISBN))
                    throw new InvalidOperationException($"Book with ISBN {bookDto.ISBN} already exists");

                var book = MapToEntity(bookDto);
                var createdBook = await _bookRepository.AddAsync(book);
                return MapToDto(createdBook);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating book: {ex.Message}", ex);
            }
        }

        public async Task<BookDto> UpdateBookAsync(int id, BookDto bookDto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid book ID", nameof(id));

                if (bookDto == null)
                    throw new ArgumentNullException(nameof(bookDto));

                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null)
                    throw new KeyNotFoundException($"Book with ID {id} not found");

                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.ISBN = bookDto.ISBN;
                book.CategoryID = bookDto.CategoryID;

                var updatedBook = await _bookRepository.UpdateAsync(book);
                return MapToDto(updatedBook);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating book: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid book ID", nameof(id));

                return await _bookRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting book: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                    throw new ArgumentException("Invalid category ID", nameof(categoryId));

                var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);
                return books.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving books by category: {ex.Message}", ex);
            }
        }

        public async Task<BookDto> GetBookByIsbnAsync(string isbn)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(isbn))
                    throw new ArgumentException("ISBN cannot be empty", nameof(isbn));

                var book = await _bookRepository.GetByIsbnAsync(isbn);
                if (book == null)
                    throw new KeyNotFoundException($"Book with ISBN {isbn} not found");

                return MapToDto(book);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving book by ISBN: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BookDto>> GetBooksWithCategoryAsync()
        {
            try
            {
                var books = await _bookRepository.GetBooksWithCategoryAsync();
                return books.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving books with category: {ex.Message}", ex);
            }
        }

        private BookDto MapToDto(Book book) => new()
        {
            ID = book.ID,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            CategoryID = book.CategoryID
        };

        private Book MapToEntity(BookDto dto) => new()
        {
            Title = dto.Title,
            Author = dto.Author,
            ISBN = dto.ISBN,
            CategoryID = dto.CategoryID
        };
    }
}
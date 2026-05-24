using LibraryCrud.Data;
using LibraryCrud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryCrud.Application.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.ID == id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Category).ToListAsync();
        }

        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _context.Books
                .Where(b => b.CategoryID == categoryId)
                .Include(b => b.Category)
                .ToListAsync();
        }

        public async Task<Book?> GetByIsbnAsync(string isbn)
        {
            return await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<IEnumerable<Book>> GetBooksWithCategoryAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .Where(b => b.Category != null)
                .ToListAsync();
        }

        public async Task<bool> IsbnExistsAsync(string isbn)
        {
            return await _context.Books.AnyAsync(b => b.ISBN == isbn);
        }
    }
}

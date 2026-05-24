using LibraryCrud.Data;
using LibraryCrud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryCrud.Application.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Categories?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Books)
                .ToListAsync();
        }

        public async Task<Categories> AddAsync(Categories category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Categories> UpdateAsync(Categories category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Categories?> GetByNameAsync(string name)
        {
            return await _context.Categories
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Categories>> GetCategoriesWithBooksAsync()
        {
            return await _context.Categories
                .Include(c => c.Books)
                .Where(c => c.Books != null && c.Books.Any()) 
                .ToListAsync();
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.Categories
                .AnyAsync(c => c.Name == name);
        }
    }
}
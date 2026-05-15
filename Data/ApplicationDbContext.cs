using Microsoft.EntityFrameworkCore;
using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<BorrowedRecords> BorrowedRecords { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

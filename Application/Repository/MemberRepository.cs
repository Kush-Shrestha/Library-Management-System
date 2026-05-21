using LibraryCrud.Data;
using LibraryCrud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryCrud.Application.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Members?> GetByIdAsync(int id)
        {
            return await _context.Members.Include(m => m.BorrowedRecords).FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task<IEnumerable<Members>> GetAllAsync()
        {
            return await _context.Members.Include(m => m.BorrowedRecords).ToListAsync();
        }

        public async Task<Members> AddAsync(Members member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Members> UpdateAsync(Members member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                return false;

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Members?> GetByEmailAsync(string email)
        {
            return await _context.Members
                .Include(m => m.BorrowedRecords)
                .FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<IEnumerable<Members>> GetMembersWithBorrowRecordsAsync()
        {
            return await _context.Members
                .Include(m => m.BorrowedRecords)
                .Where(m => m.BorrowedRecords.Count > 0)
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Members.AnyAsync(m => m.Email == email);
        }
    }
}

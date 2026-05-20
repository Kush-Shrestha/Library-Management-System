using LibraryCrud.Data;
using LibraryCrud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryCrud.Application.Repository
{
    public class BorrowedRecordRepository : IBorrowedRecordsRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowedRecordRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<BorrowedRecords> GetByIdAsync(int id)
        {
            return await _context.BorrowedRecords
                .Include(br => br.Book)
                .Include(br => br.Member)
                .FirstOrDefaultAsync(br => br.ID == id);
        }

        public async Task<BorrowedRecords> GetDetailedRecordAsync(int id)
        {
            return await _context.BorrowedRecords
                .Include(br => br.Book)
                .ThenInclude(b => b.Category)
                .Include(br => br.Member)
                .FirstOrDefaultAsync(br => br.ID == id);
        }

        public async Task<IEnumerable<BorrowedRecords>> GetAllAsync()
        {
            return await _context.BorrowedRecords
                .Include(br => br.Book)
                .Include(br => br.Member)
                .ToListAsync();
        }

        public async Task<BorrowedRecords> AddAsync(BorrowedRecords record)
        {
            _context.BorrowedRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<BorrowedRecords> UpdateAsync(BorrowedRecords record)
        {
            _context.BorrowedRecords.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.BorrowedRecords.FindAsync(id);
            if (record == null)
                return false;

            _context.BorrowedRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BorrowedRecords>> GetByMemberIdAsync(int memberId)
        {
            return await _context.BorrowedRecords
                .Where(br => br.MemberID == memberId)
                .Include(br => br.Book)
                .Include(br => br.Member)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowedRecords>> GetByBookIdAsync(int bookId)
        {
            return await _context.BorrowedRecords
                .Where(br => br.BookID == bookId)
                .Include(br => br.Book)
                .Include(br => br.Member)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowedRecords>> GetUnreturnedRecordsAsync()
        {
            return await _context.BorrowedRecords
                .Where(br => br.ReturnDate == null)
                .Include(br => br.Book)
                .Include(br => br.Member)
                .ToListAsync();
        }
    }
}

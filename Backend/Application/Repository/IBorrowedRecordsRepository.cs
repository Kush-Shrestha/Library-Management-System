using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Application.Repository
{
    public interface IBorrowedRecordsRepository
    {
        Task<BorrowedRecords?> GetByIdAsync(int id);
        Task<BorrowedRecords?> GetDetailedRecordAsync(int id); 
        Task<IEnumerable<BorrowedRecords>> GetAllAsync();
        Task<BorrowedRecords> AddAsync(BorrowedRecords record);
        Task<BorrowedRecords> UpdateAsync(BorrowedRecords record);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<BorrowedRecords>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<BorrowedRecords>> GetByBookIdAsync(int bookId);
        Task<IEnumerable<BorrowedRecords>> GetUnreturnedRecordsAsync();
    }
}
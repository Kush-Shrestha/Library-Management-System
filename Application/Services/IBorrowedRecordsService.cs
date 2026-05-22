using LibraryCrud.Domain.DTOs;

namespace LibraryCrud.Application.Services
{
    public interface IBorrowedRecordsService
    {
        Task<BorrowedRecordDto> GetRecordByIdAsync(int id);
        Task<IEnumerable<BorrowedRecordDto>> GetAllRecordsAsync();
        Task<BorrowedRecordDto> CreateBorrowRecordAsync(BorrowedRecordDto recordDto);
        Task<BorrowedRecordDto> ReturnBookAsync(int recordId);
        Task<bool> DeleteRecordAsync(int id);
        Task<IEnumerable<BorrowedRecordDto>> GetMemberBorrowHistoryAsync(int memberId);
        Task<IEnumerable<BorrowedRecordDto>> GetBookBorrowHistoryAsync(int bookId);
        Task<IEnumerable<BorrowedRecordDto>> GetUnreturnedBooksAsync();
    }
}

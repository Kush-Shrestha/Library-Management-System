using LibraryCrud.Application.Repository;
using LibraryCrud.Domain.DTOs;
using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Application.Services
{
    public class BorrowedRecordsService : IBorrowedRecordsService
    {
        private readonly IBorrowedRecordsRepository _borrowedRecordsRepository;

        public BorrowedRecordsService(IBorrowedRecordsRepository borrowedRecordsRepository)
        {
            _borrowedRecordsRepository = borrowedRecordsRepository ?? throw new ArgumentNullException(nameof(borrowedRecordsRepository));
        }

        public async Task<BorrowedRecordDto> GetRecordByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid record ID", nameof(id));

                var record = await _borrowedRecordsRepository.GetDetailedRecordAsync(id);
                if (record == null)
                    throw new KeyNotFoundException($"Borrow record with ID {id} not found");

                return MapToDto(record);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving borrow record: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BorrowedRecordDto>> GetAllRecordsAsync()
        {
            try
            {
                var records = await _borrowedRecordsRepository.GetAllAsync();
                return records.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all borrow records: {ex.Message}", ex);
            }
        }

        public async Task<BorrowedRecordDto> CreateBorrowRecordAsync(BorrowedRecordDto recordDto)
        {
            try
            {
                if (recordDto == null)
                    throw new ArgumentNullException(nameof(recordDto));

                var record = MapToEntity(recordDto);
                var createdRecord = await _borrowedRecordsRepository.AddAsync(record);
                return MapToDto(createdRecord);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating borrow record: {ex.Message}", ex);
            }
        }

        public async Task<BorrowedRecordDto> ReturnBookAsync(int recordId)
        {
            try
            {
                if (recordId <= 0)
                    throw new ArgumentException("Invalid record ID", nameof(recordId));

                var record = await _borrowedRecordsRepository.GetByIdAsync(recordId);
                if (record == null)
                    throw new KeyNotFoundException($"Borrow record with ID {recordId} not found");

                record.ReturnDate = DateTime.UtcNow;
                var updatedRecord = await _borrowedRecordsRepository.UpdateAsync(record);
                return MapToDto(updatedRecord);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error returning book: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteRecordAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid record ID", nameof(id));

                return await _borrowedRecordsRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting borrow record: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BorrowedRecordDto>> GetMemberBorrowHistoryAsync(int memberId)
        {
            try
            {
                if (memberId <= 0)
                    throw new ArgumentException("Invalid member ID", nameof(memberId));

                var records = await _borrowedRecordsRepository.GetByMemberIdAsync(memberId);
                return records.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving member borrow history: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BorrowedRecordDto>> GetBookBorrowHistoryAsync(int bookId)
        {
            try
            {
                if (bookId <= 0)
                    throw new ArgumentException("Invalid book ID", nameof(bookId));

                var records = await _borrowedRecordsRepository.GetByBookIdAsync(bookId);
                return records.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving book borrow history: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BorrowedRecordDto>> GetUnreturnedBooksAsync()
        {
            try
            {
                var records = await _borrowedRecordsRepository.GetUnreturnedRecordsAsync();
                return records.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving unreturned books: {ex.Message}", ex);
            }
        }

        private BorrowedRecordDto MapToDto(BorrowedRecords record) => new()
        {
            ID = record.ID,
            BookID = record.BookID,
            MemberID = record.MemberID,
            BorrowDate = record.BorrowDate,
            ReturnDate = record.ReturnDate
        };
               //"Convert a BorrowedRecordDto into a BorrowedRecords (entity)"
        private BorrowedRecords MapToEntity(BorrowedRecordDto dto) => new()
        {
            ID = 0,
            BookID = dto.BookID,
            MemberID = dto.MemberID,
            BorrowDate = dto.BorrowDate,
            ReturnDate = dto.ReturnDate,
            Book = null!,
            Member = null!
        };
    }
}

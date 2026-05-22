using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Application.Repository
{
    public interface IMemberRepository
    {
        Task<Members?> GetByIdAsync(int id);
        Task<IEnumerable<Members>> GetAllAsync();
        Task<Members> AddAsync(Members member);
        Task<Members> UpdateAsync(Members member);
        Task<bool> DeleteAsync(int id);
        Task<Members?> GetByEmailAsync(string email); 
        Task<IEnumerable<Members>> GetMembersWithBorrowRecordsAsync();
        Task<bool> EmailExistsAsync(string email);
    }
}
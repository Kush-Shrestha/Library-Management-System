using LibraryCrud.Domain.DTOs;

namespace LibraryCrud.Application.Services
{
    public interface IMemberService
    {
        Task<MemberDto> GetMemberByIdAsync(int id);
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();
        Task<MemberDto> CreateMemberAsync(MemberDto memberDto);
        Task<MemberDto> UpdateMemberAsync(int id, MemberDto memberDto);
        Task<bool> DeleteMemberAsync(int id);
        Task<MemberDto> GetMemberByEmailAsync(string email);
        Task<IEnumerable<MemberDto>> GetMembersWithBorrowRecordsAsync();
    }
}

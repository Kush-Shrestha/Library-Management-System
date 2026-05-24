using LibraryCrud.Application.Repository;
using LibraryCrud.Domain.DTOs;
using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        }

        public async Task<MemberDto> GetMemberByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid member ID", nameof(id));

                var member = await _memberRepository.GetByIdAsync(id);
                if (member == null)
                    throw new KeyNotFoundException($"Member with ID {id} not found");

                return MapToDto(member);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving member: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            try
            {
                var members = await _memberRepository.GetAllAsync();
                return members.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all members: {ex.Message}", ex);
            }
        }

        public async Task<MemberDto> CreateMemberAsync(MemberDto memberDto)
        {
            try
            {
                if (memberDto == null)
                    throw new ArgumentNullException(nameof(memberDto));

                // Validate email uniqueness
                if (await _memberRepository.EmailExistsAsync(memberDto.Email))
                    throw new InvalidOperationException($"Member with email {memberDto.Email} already exists");

                var member = MapToEntity(memberDto);
                var createdMember = await _memberRepository.AddAsync(member);
                return MapToDto(createdMember);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating member: {ex.Message}", ex);
            }
        }

        public async Task<MemberDto> UpdateMemberAsync(int id, MemberDto memberDto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid member ID", nameof(id));

                if (memberDto == null)
                    throw new ArgumentNullException(nameof(memberDto));

                var member = await _memberRepository.GetByIdAsync(id);
                if (member == null)
                    throw new KeyNotFoundException($"Member with ID {id} not found");

                member.FullName = memberDto.FullName;
                member.Email = memberDto.Email;
                member.PhoneNumber = memberDto.PhoneNumber;

                var updatedMember = await _memberRepository.UpdateAsync(member);
                return MapToDto(updatedMember);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating member: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid member ID", nameof(id));

                return await _memberRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting member: {ex.Message}", ex);
            }
        }

        public async Task<MemberDto> GetMemberByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Email cannot be empty", nameof(email));

                var member = await _memberRepository.GetByEmailAsync(email);
                if (member == null)
                    throw new KeyNotFoundException($"Member with email {email} not found");

                return MapToDto(member);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving member by email: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<MemberDto>> GetMembersWithBorrowRecordsAsync()
        {
            try
            {
                var members = await _memberRepository.GetMembersWithBorrowRecordsAsync();
                return members.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving members with borrow records: {ex.Message}", ex);
            }
        }

        private MemberDto MapToDto(Members member) => new()
        {
            ID = member.ID,
            FullName = member.FullName,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber
        };

        private Members MapToEntity(MemberDto dto) => new()
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
    }
}

using LibraryCrud.Application.Services;
using LibraryCrud.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                    return NotFound(new { message = "Member not found" });
                return Ok(member);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            try
            {
                var members = await _memberService.GetAllMembersAsync();
                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberDto createMemberDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var memberDto = new MemberDto
                {
                    FullName = createMemberDto.FullName,
                    Email = createMemberDto.Email,
                    PhoneNumber = createMemberDto.PhoneNumber
                };

                var createdMember = await _memberService.CreateMemberAsync(memberDto);
                return CreatedAtAction(nameof(GetMemberById), new { id = createdMember.ID }, createdMember);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] UpdateMemberDto updateMemberDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var memberDto = new MemberDto
                {
                    ID = id,
                    FullName = updateMemberDto.FullName,
                    Email = updateMemberDto.Email,
                    PhoneNumber = updateMemberDto.PhoneNumber
                };

                var updatedMember = await _memberService.UpdateMemberAsync(id, memberDto);
                if (updatedMember == null)
                    return NotFound(new { message = "Member not found" });
                return Ok(updatedMember);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            {
                var result = await _memberService.DeleteMemberAsync(id);
                if (!result)
                    return NotFound(new { message = "Member not found" });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetMemberByEmail(string email)
        {
            try
            {
                var member = await _memberService.GetMemberByEmailAsync(email);
                if (member == null)
                    return NotFound(new { message = "Member not found" });
                return Ok(member);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("with-borrow-records")]
        public async Task<IActionResult> GetMembersWithBorrowRecords()
        {
            try
            {
                var members = await _memberService.GetMembersWithBorrowRecordsAsync();
                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }
    }
}

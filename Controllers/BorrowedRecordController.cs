using LibraryCrud.Application.Services;
using LibraryCrud.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedRecordController : ControllerBase
    {
        private readonly IBorrowedRecordsService _borrowedRecordsService;

        public BorrowedRecordController(IBorrowedRecordsService borrowedRecordsService)
        {
            _borrowedRecordsService = borrowedRecordsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecords()
        {
            try
            {
                var records = await _borrowedRecordsService.GetAllRecordsAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordById(int id)
        {
            try
            {
                var record = await _borrowedRecordsService.GetRecordByIdAsync(id);
                if (record == null)
                    return NotFound(new { message = "Record not found" });
                return Ok(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBorrowRecord([FromBody] CreateBorrowedRecordDto createRecordDto)
        {   /* [FromBody] tells the API
            "Take the data from the HTTP request body and put it into this parameter." */
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var recordDto = new BorrowedRecordDto
                {
                    ID = 0,
                    BookID = createRecordDto.BookID,
                    MemberID = createRecordDto.MemberID,
                    BorrowDate = createRecordDto.BorrowDate
                };

                var createdRecord = await _borrowedRecordsService.CreateBorrowRecordAsync(recordDto);
                return CreatedAtAction(nameof(GetRecordById), new { id = createdRecord.ID }, createdRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrowRecord(int id, [FromBody] UpdateBorrowedRecordDto updateRecordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var recordDto = new BorrowedRecordDto
                {
                    ID = id,
                    BookID = 0,
                    MemberID = 0,
                    BorrowDate = DateTime.MinValue,
                    ReturnDate = updateRecordDto.ReturnDate
                };

                var updatedRecord = await _borrowedRecordsService.ReturnBookAsync(id);
                if (updatedRecord == null)
                    return NotFound(new { message = "Record not found" });
                return Ok(updatedRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            try
            {
                var result = await _borrowedRecordsService.DeleteRecordAsync(id);
                if (!result)
                    return NotFound(new { message = "Record not found" });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetMemberBorrowHistory(int memberId)
        {
            try
            {
                var records = await _borrowedRecordsService.GetMemberBorrowHistoryAsync(memberId);
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetBookBorrowHistory(int bookId)
        {
            try
            {
                var records = await _borrowedRecordsService.GetBookBorrowHistoryAsync(bookId);
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("unreturned")]
        public async Task<IActionResult> GetUnreturnedBooks()
        {
            try
            {
                var records = await _borrowedRecordsService.GetUnreturnedBooksAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }
    }
}

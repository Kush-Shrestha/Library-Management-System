using LibraryCrud.Application.Services;
using LibraryCrud.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                    return NotFound(new { message = "Book not found" });
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var bookDto = new BookDto
                {
                    Title = createBookDto.Title,
                    Author = createBookDto.Author,
                    ISBN = createBookDto.ISBN,
                    CategoryID = createBookDto.CategoryID
                };

                var createdBook = await _bookService.CreateBookAsync(bookDto);
                return CreatedAtAction(nameof(GetBookById), new { id = createdBook.ID }, createdBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var bookDto = new BookDto
                {
                    ID = id,
                    Title = updateBookDto.Title,
                    Author = updateBookDto.Author,
                    ISBN = updateBookDto.ISBN,
                    CategoryID = updateBookDto.CategoryID
                };

                var updatedBook = await _bookService.UpdateBookAsync(id, bookDto);
                if (updatedBook == null)
                    return NotFound(new { message = "Book not found" });
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var result = await _bookService.DeleteBookAsync(id);
                if (!result)
                    return NotFound(new { message = "Book not found" });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            try
            {
                var books = await _bookService.GetBooksByCategoryAsync(categoryId);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IActionResult> GetBookByIsbn(string isbn)
        {
            try
            {
                var book = await _bookService.GetBookByIsbnAsync(isbn);
                if (book == null)
                    return NotFound(new { message = "Book not found" });
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("with-category")]
        public async Task<IActionResult> GetBooksWithCategory()
        {
            try
            {
                var books = await _bookService.GetBooksWithCategoryAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using LibraryCrud.Domain.DTOs;
using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new();
        private static int id = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(books);
        }

        [HttpPost]
        public IActionResult Create(CreateBookDto dto)
        {
            var book = new Book { ID = id++, Title = dto.Title, Author = dto.Author, ISBN = dto.ISBN };
            books.Add(book);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBookDto dto)
        {
            var book = books.FirstOrDefault(x => x.ID == id);
            if (book == null) return NotFound();
            book.Title = dto.Title;
            book.Author = dto.Author;
            book.ISBN = dto.ISBN;
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x => x.ID == id);
            if (book == null) return NotFound();
            books.Remove(book);
            return Ok("Deleted");
        }
    }

    
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private static List<Categories> categories = new();
        private static int id = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto dto)
        {
            var category = new Categories { ID = id++, Name = dto.Name };
            categories.Add(category);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCategoryDto dto)
        {
            var category = categories.FirstOrDefault(x => x.ID == id);
            if (category == null) return NotFound();
            category.Name = dto.Name;
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = categories.FirstOrDefault(x => x.ID == id);
            if (category == null) return NotFound();
            categories.Remove(category);
            return Ok("Deleted");
        }
    }


    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private static List<Members> members = new();
        private static int id = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(members);
        }

        [HttpPost]
        public IActionResult Create(CreateMemberDto dto)
        {
            var member = new Members { ID = id++, FullName = dto.FullName, Email = dto.Email, PhoneNumber = dto.PhoneNumber };
            members.Add(member);
            return Ok(member);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateMemberDto dto)
        {
            var member = members.FirstOrDefault(x => x.ID == id);
            if (member == null) return NotFound();
            member.FullName = dto.FullName;
            member.Email = dto.Email;
            member.PhoneNumber = dto.PhoneNumber;
            return Ok(member);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var member = members.FirstOrDefault(x => x.ID == id);
            if (member == null) return NotFound();
            members.Remove(member);
            return Ok("Deleted");
        }
    }

   
    [Route("api/[controller]")]
    public class BorrowedRecordsController : ControllerBase
    {
        private static List<BorrowedRecords> records = new();
        private static int id = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(records);
        }

        [HttpPost]
        public IActionResult Create(CreateBorrowedRecordDto dto)
        {
            var record = new BorrowedRecords { ID = id++, BookID = dto.BookID, MemberID = dto.MemberID, BorrowDate = dto.BorrowDate };
            records.Add(record);
            return Ok(record);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBorrowedRecordDto dto)
        {
            var record = records.FirstOrDefault(x => x.ID == id);
            if (record == null) return NotFound();
            record.ReturnDate = dto.ReturnDate;
            return Ok(record);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var record = records.FirstOrDefault(x => x.ID == id);
            if (record == null) return NotFound();
            records.Remove(record);
            return Ok("Deleted");
        }
    }

  
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new();
        private static int id = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Create(CreateUserDto dto)
        {
            var user = new User { ID = id++, Name = dto.Name, Email = dto.Email, Password = dto.Password };
            users.Add(user);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateUserDto dto)
        {
            var user = users.FirstOrDefault(x => x.ID == id);
            if (user == null) return NotFound();
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Password = dto.Password;
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(x => x.ID == id);
            if (user == null) return NotFound();
            users.Remove(user);
            return Ok("Deleted");
        }
    }
}



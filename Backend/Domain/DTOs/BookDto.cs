namespace LibraryCrud.Domain.DTOs
{
    public class BookDto
    {
    
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
    }

    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
    }

    public class UpdateBookDto
    {
        public string Title { get; set; }   
        public string Author { get; set; }
        public string ISBN { get; set; }
    }
}

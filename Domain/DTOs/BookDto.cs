namespace LibraryCrud.Domain.DTOs
{
    public class BookDto
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public int CategoryID { get; set; }
    }

    public class CreateBookDto
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required int CategoryID { get; set; }
    }

    public class UpdateBookDto
    {
        public required string Title { get; set; }   
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required int CategoryID { get; set; }
    }
}

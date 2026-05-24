namespace LibraryCrud.Domain.Entity
{
    public class Categories
    {
        public required int ID { get; set; }
        public required string Name { get; set; }

        // Navigation property for books in this category
        public ICollection<Book>? Books { get; set; }
    }
}

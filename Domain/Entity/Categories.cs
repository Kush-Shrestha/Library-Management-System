namespace LibraryCrud.Domain.Entity
{
    public class Categories
    {
        public int ID { get; set; }
        public string Name { get; set; }

        // Navigation property for books in this category
        public ICollection<Book> Books { get; set; }
    }
}

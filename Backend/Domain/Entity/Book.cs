namespace LibraryCrud.Domain.Entity
{
    public class Book
    {

        public int ID { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public string? ISBN { get; set; } // International Standard Book Number

        // Foreign Key to Category
        public required int CategoryID { get; set; }

        // Navigation property
        public required Categories Category { get; set; }

        // Navigation property for borrowed records
        public ICollection<BorrowedRecords>? BorrowedRecords { get; set; }
    }
}

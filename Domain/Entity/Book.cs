namespace LibraryCrud.Domain.Entity
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; } // International Standard Book Number

        // Foreign Key to Category
        public int CategoryID { get; set; }

        // Navigation property
        public Categories Category { get; set; }

        // Navigation property for borrowed records
        public ICollection<BorrowedRecords> BorrowedRecords { get; set; }
    }
}

namespace LibraryCrud.Domain.Entity
{
    public class BorrowedRecords
    {
        public required int ID { get; set; }
        public required int BookID { get; set; }
        public required int MemberID { get; set; }
        public required DateTime BorrowDate { get; set; }
        public required DateTime ReturnDate { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public Members Member { get; set; }
    }
}

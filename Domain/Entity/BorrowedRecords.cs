namespace LibraryCrud.Domain.Entity
{
    public class BorrowedRecords
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public Members Member { get; set; }
    }
}

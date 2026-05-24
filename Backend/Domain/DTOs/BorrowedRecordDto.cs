namespace LibraryCrud.Domain.DTOs
{
    public class BorrowedRecordDto
    {
        public int ID { get; set; }
        public required int BookID { get; set; }
        public required int MemberID { get; set; }
        public required DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class CreateBorrowedRecordDto
    {
        public required int BookID { get; set; }
        public required int MemberID { get; set; }
        public required DateTime BorrowDate { get; set; }
    }

    public class UpdateBorrowedRecordDto
    {
        public required DateTime? ReturnDate { get; set; }
    }
}

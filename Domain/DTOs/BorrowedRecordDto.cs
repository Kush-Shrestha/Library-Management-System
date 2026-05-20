namespace LibraryCrud.Domain.DTOs
{
    public class BorrowedRecordDto
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class CreateBorrowedRecordDto
    {
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
    }

    public class UpdateBorrowedRecordDto
    {
        public DateTime? ReturnDate { get; set; }
    }
}

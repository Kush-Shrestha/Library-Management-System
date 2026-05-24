namespace LibraryCrud.Domain.Entity
{
    public class Members
    {
        public int ID { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        // Navigation property for borrowed records
        public ICollection<BorrowedRecords>? BorrowedRecords { get; set; }
    }
}

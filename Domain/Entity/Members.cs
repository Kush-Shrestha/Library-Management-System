namespace LibraryCrud.Domain.Entity
{
    public class Members
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation property for borrowed records
        public ICollection<BorrowedRecords> BorrowedRecords { get; set; }
    }
}

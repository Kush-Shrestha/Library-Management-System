namespace LibraryCrud.Domain.DTOs
{
    public class MemberDto
    {
        public int ID { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }

    public class CreateMemberDto
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }

    public class UpdateMemberDto
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }
}

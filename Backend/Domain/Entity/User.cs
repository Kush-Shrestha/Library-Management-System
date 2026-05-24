namespace LibraryCrud.Domain.Entity
{
    public class User // here the user means the librarian 
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}

namespace LibraryCrud.Domain.Entity
{
    public class User // here the user means the librarian 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

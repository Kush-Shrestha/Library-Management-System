namespace LibraryCrud.Domain.DTOs
{
    public class CategoryDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; }
    }

    public class UpdateCategoryDto
    {
        public string Name { get; set; }
    }
}

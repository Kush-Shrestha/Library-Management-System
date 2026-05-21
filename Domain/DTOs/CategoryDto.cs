namespace LibraryCrud.Domain.DTOs
{
    public class CategoryDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
    }

    public class CreateCategoryDto
    {
        public required string Name { get; set; }
    }

    public class UpdateCategoryDto
    {
        public required string Name { get; set; }
    }
}

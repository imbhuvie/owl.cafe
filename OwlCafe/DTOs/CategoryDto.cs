namespace OwlCafe.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public int DisplayOrder { get; set; }
        public bool Status { get; set; }
    }
}

namespace OwlCafe.DTOs
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? Image { get; set; }
        public bool IsVeg { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsFeatured { get; set; }
        public int PreparationTime { get; set; }
        public bool Status { get; set; }
        public int DisplayOrder { get; set; }
    }
}

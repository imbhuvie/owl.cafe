namespace OwlCafe.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? Image { get; set; }
        public bool IsVeg { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsFeatured { get; set; }
        public int PreparationTime { get; set; } // in minutes
        public bool Status { get; set; } = true;
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public Category? Category { get; set; }
    }
}

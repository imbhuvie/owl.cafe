namespace OwlCafe.Models
{
    public class WebsiteSetting
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; } = "Owl Cafe";
        public string? Logo { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string OpeningHours { get; set; } = string.Empty;
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? WhatsApp { get; set; }
        public string? GoogleMap { get; set; }
        public string? SEODescription { get; set; }
        public string? SEOKeywords { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}

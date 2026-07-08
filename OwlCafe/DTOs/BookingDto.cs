using OwlCafe.Models;

namespace OwlCafe.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public int Guests { get; set; }
        public int? TableId { get; set; }
        public string TableNumber { get; set; } = string.Empty;
        public string? SpecialRequest { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

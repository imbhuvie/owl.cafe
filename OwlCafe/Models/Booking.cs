namespace OwlCafe.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingTime { get; set; }
        public int Guests { get; set; }
        public int? TableId { get; set; }
        public string? SpecialRequest { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public RestaurantTable? Table { get; set; }
    }

    public enum BookingStatus
    {
        Pending,
        Approved,
        Rejected,
        Completed,
        Cancelled
    }
}

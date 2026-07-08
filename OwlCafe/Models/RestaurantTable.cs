namespace OwlCafe.Models
{
    public class RestaurantTable
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; } = string.Empty;
        public TableStatus Status { get; set; } = TableStatus.Available;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

    public enum TableStatus
    {
        Available,
        Reserved,
        Maintenance,
        Occupied
    }
}

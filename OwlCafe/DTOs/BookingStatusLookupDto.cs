using System.ComponentModel.DataAnnotations;

namespace OwlCafe.DTOs
{
    public class BookingStatusLookupDto
    {
        [Required]
        [Display(Name = "Confirmation Number")]
        public int BookingId { get; set; }

        [Required]
        [Display(Name = "Phone or Email")]
        public string Contact { get; set; } = string.Empty;
    }
}

namespace CLDV6211_WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public int VenueID { get; set; }
        public Venue Venue { get; set; }

        [Required]
        public int EventID { get; set; }
        public Event Event { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Booking price must be positive.")]
        public decimal BookingPrice { get; set; }
    }

}

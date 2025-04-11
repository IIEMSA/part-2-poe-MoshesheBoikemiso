using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CLDV6211_WebApplication.Models;
using System.ComponentModel.DataAnnotations;


namespace CLDV6211_WebApplication.Models
{
    public class Venue
    {
        public int VenueID { get; set; }

        [Required(ErrorMessage = "Venue Name is required")]
        public string VenueName { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
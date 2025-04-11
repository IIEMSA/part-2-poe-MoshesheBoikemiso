using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CLDV6211_WebApplication.Models;


namespace CLDV6211_WebApplication.Models
{
    [Table("EventEntity")]
    public class Event
    {
        public int EventID { get; set; }

        public string EventName { get; set; }

        [Column("EventStartDate")]
        public DateTime StartDate { get; set; }

        [Column("EventEndDate")]
        public DateTime EndDate { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}

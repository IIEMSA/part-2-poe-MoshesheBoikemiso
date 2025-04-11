using Microsoft.EntityFrameworkCore;

namespace CLDV6211_WebApplication.Models
{
    public class WebAppDBContext : DbContext
    {
        public WebAppDBContext(DbContextOptions<WebAppDBContext> options) : base(options)
        {
        }


        public DbSet<Venue> Venues { get; set; }

        public DbSet<Event> Events { get; set; } 

        
        public DbSet<Booking> Bookings { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("EventEntity");  
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace TravelAgency.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserModel> User { get; set; }
        public DbSet<BookingModel> Booking { get; set; }
        public DbSet<PaymentModel> Payment { get; set; }
        public DbSet<TravelerModel> Traveler { get; set; }
        public DbSet<TravelPackageModel> TravelPackage { get; set; }
    }
}

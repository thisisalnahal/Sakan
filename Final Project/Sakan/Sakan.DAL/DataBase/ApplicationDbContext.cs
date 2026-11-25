using Microsoft.EntityFrameworkCore;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Listing> Listings => Set<Listing>();
        public DbSet<ListingPrice> ListingPrices => Set<ListingPrice>();
        public DbSet<ListingImage> ListingImages => Set<ListingImage>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingStatusHistory> BookingStatusHistories => Set<BookingStatusHistory>();
        public DbSet<AvailabilityBlock> AvailabilityBlocks => Set<AvailabilityBlock>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Sakan;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
    }
}

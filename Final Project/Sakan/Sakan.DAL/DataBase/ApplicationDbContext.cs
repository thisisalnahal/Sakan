using Microsoft.EntityFrameworkCore;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase
{
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Sakan;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        
        public DbSet<ApplicationUser> Users { get; set; } = null!;
        public DbSet<IdentityVerification> IdentityVerifications { get; set; } = null!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        
        public DbSet<Listing> Listings => Set<Listing>();
        public DbSet<ListingPrice> ListingPrices => Set<ListingPrice>();
        public DbSet<ListingImage> ListingImages => Set<ListingImage>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingStatusHistory> BookingStatusHistories => Set<BookingStatusHistory>();
        public DbSet<AvailabilityBlock> AvailabilityBlocks => Set<AvailabilityBlock>();
        
        //finance 
        public DbSet<PaymentAuthorization> PaymentAuthorizations { get; set; }
        public DbSet<SecurityDeposit> SecurityDeposits { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<HostPayout> HostPayouts { get; set; }
        public DbSet<HostPenalty> HostPenalties { get; set; }
        public DbSet<HostPenaltyDeduction> HostPenaltyDeductions { get; set; }
        
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<DisputeEvidence> DisputeEvidences { get; set; }
        public DbSet<DisputeResolution> DisputeResolutions { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<ListingReview> ListingReviews { get; set; }
    }
}

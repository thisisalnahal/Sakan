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
         public DbSet<Dispute> Disputes { get; set; }
         public DbSet<DisputeEvidence> DisputeEvidences { get; set; }
        public DbSet<DisputeResolution> DisputeResolutions { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<ListingReview> ListingReviews { get; set; }
    }
}

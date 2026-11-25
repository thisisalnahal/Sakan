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
    }
}

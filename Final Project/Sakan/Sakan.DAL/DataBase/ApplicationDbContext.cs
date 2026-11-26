using Microsoft.EntityFrameworkCore;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase
{
    internal class ApplicationDbContext : DbContext
    {
        // M2
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyAddress> PropertyAddresses { get; set; }
        public DbSet<RentalUnit> RentalUnits { get; set; }
        public DbSet<SharedSpace> SharedSpaces { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        public DbSet<UnitAmenity> UnitAmenities { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public DbSet<CancellationPolicyRule> CancellationPolicyRules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Sakan;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
    }
} 


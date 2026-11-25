using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    public class ListingConfiguration : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Title).IsRequired().HasMaxLength(200);
            builder.Property(l => l.Description).IsRequired();
            builder.Property(l => l.Currency).IsRequired().HasMaxLength(3).IsFixedLength();
            builder.Property(l => l.SecurityDepositAmount).HasPrecision(18, 2);
            builder.Property(l => l.Status).HasConversion<string>();

            builder.HasIndex(l => l.RentalUnitId).IsUnique();
        }
    }
}
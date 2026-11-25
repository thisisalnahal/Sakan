using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    public class ListingPriceConfiguration : IEntityTypeConfiguration<ListingPrice>
    {
        public void Configure(EntityTypeBuilder<ListingPrice> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PricingPeriod).HasConversion<string>();
            builder.Property(p => p.Price).HasPrecision(18, 2);

            builder.HasIndex(p => new { p.ListingId, p.PricingPeriod }).IsUnique();

            builder.HasOne(p => p.Listing)
                   .WithMany(l => l.Prices)
                   .HasForeignKey(p => p.ListingId);
        }
    }
}
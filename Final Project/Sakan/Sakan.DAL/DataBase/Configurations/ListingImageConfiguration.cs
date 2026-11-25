using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    public class ListingImageConfiguration : IEntityTypeConfiguration<ListingImage>
    {
        public void Configure(EntityTypeBuilder<ListingImage> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.StorageKey).IsRequired().HasMaxLength(500);

            builder.HasIndex(i => new { i.ListingId, i.IsPrimary })
                   .IsUnique()
                   .HasFilter("[IsPrimary] = 1");

            builder.HasOne(i => i.Listing)
                   .WithMany(l => l.Images)
                   .HasForeignKey(i => i.ListingId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
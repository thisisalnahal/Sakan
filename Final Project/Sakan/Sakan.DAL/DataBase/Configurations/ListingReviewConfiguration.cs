using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class ListingReviewConfiguration : IEntityTypeConfiguration<ListingReview>
    {
        public void Configure(EntityTypeBuilder<ListingReview> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating)
                   .IsRequired();

            builder.Property(r => r.CreatedAt)
                   .IsRequired();

            builder.HasIndex(r => r.BookingId)
                   .IsUnique();

            builder.ToTable(t => t.HasCheckConstraint("CK_ListingReview_Rating", "[Rating] BETWEEN 1 AND 5"));

            builder.HasOne(r => r.Booking)
                   .WithOne()
                   .HasForeignKey<ListingReview>(r => r.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

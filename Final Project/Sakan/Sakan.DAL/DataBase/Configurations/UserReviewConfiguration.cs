using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class UserReviewConfiguration : IEntityTypeConfiguration<UserReview>
    {
        public void Configure(EntityTypeBuilder<UserReview> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.ReviewerId)
                   .IsRequired();

            builder.Property(r => r.RevieweeId)
                   .IsRequired();

            builder.Property(r => r.Rating)
                   .IsRequired();

            builder.Property(r => r.CreatedAt)
                   .IsRequired();

            builder.HasIndex(r => new { r.BookingId, r.ReviewerId })
                   .IsUnique();

            builder.ToTable(t => t.HasCheckConstraint("CK_UserReview_Rating", "[Rating] BETWEEN 1 AND 5"));

            builder.HasOne(r => r.Booking)
                   .WithMany()
                   .HasForeignKey(r => r.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Reviewer)
                   .WithMany()
                   .HasForeignKey(r => r.ReviewerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Reviewee)
                   .WithMany()
                   .HasForeignKey(r => r.RevieweeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

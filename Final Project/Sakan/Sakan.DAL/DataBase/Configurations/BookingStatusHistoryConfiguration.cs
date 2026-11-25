using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    public class BookingStatusHistoryConfiguration : IEntityTypeConfiguration<BookingStatusHistory>
    {
        public void Configure(EntityTypeBuilder<BookingStatusHistory> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.OldStatus).HasConversion<string>();
            builder.Property(h => h.NewStatus).HasConversion<string>();
            builder.Property(h => h.Reason).HasMaxLength(500);

            builder.HasIndex(h => h.BookingId);

            builder.HasOne(h => h.Booking)
                   .WithMany(b => b.StatusHistory)
                   .HasForeignKey(h => h.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.ChangedBy)
                   .WithMany()
                   .HasForeignKey(h => h.ChangedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
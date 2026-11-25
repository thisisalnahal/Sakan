using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.PricingBreakdown).HasColumnType("nvarchar(max)");
            builder.Property(b => b.Currency).IsRequired().HasMaxLength(3).IsFixedLength();
            builder.Property(b => b.Status).HasConversion<string>();

            builder.Property(b => b.RentalAmount).HasPrecision(18, 2);
            builder.Property(b => b.SecurityDepositAmount).HasPrecision(18, 2);
            builder.Property(b => b.CommissionRate).HasPrecision(5, 4);
            builder.Property(b => b.TotalAmount)
                   .HasPrecision(18, 2)
                   .HasComputedColumnSql("[RentalAmount] + [SecurityDepositAmount]", stored: true);

            builder.ToTable(t => t.HasCheckConstraint("CK_Booking_CheckOut_After_CheckIn", "[CheckOut] > [CheckIn]"));

            builder.HasIndex(b => new { b.ListingId, b.CheckIn, b.CheckOut });

            builder.HasOne(b => b.Listing)
                   .WithMany(l => l.Bookings)
                   .HasForeignKey(b => b.ListingId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Guest)
                   .WithMany()
                   .HasForeignKey(b => b.GuestId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.CancelledBy)
                   .WithMany()
                   .HasForeignKey(b => b.CancelledById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
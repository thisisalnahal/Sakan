using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    public class AvailabilityBlockConfiguration : IEntityTypeConfiguration<AvailabilityBlock>
    {
        public void Configure(EntityTypeBuilder<AvailabilityBlock> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.BlockType).HasConversion<string>();
            builder.Property(a => a.Reason).HasMaxLength(500);

            builder.ToTable(t => t.HasCheckConstraint("CK_AvailabilityBlock_EndDate_After_StartDate", "[EndDate] > [StartDate]"));

            builder.HasIndex(a => new { a.RentalUnitId, a.StartDate, a.EndDate });

            builder.HasOne(a => a.Booking)
                   .WithMany(b => b.AvailabilityBlocks)
                   .HasForeignKey(a => a.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.CreatedBy)
                   .WithMany()
                   .HasForeignKey(a => a.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
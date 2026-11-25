using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class DisputeConfiguration : IEntityTypeConfiguration<Dispute>
    {
        public void Configure(EntityTypeBuilder<Dispute> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.DisputeType)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(d => d.ReportedById)
                   .IsRequired();

            builder.Property(d => d.Category)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(d => d.Description)
                   .IsRequired();

            builder.Property(d => d.ClaimedAmount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(d => d.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(d => d.CreatedAt)
                   .IsRequired();

            builder.HasOne(d => d.Booking)
                   .WithMany()
                   .HasForeignKey(d => d.BookingId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ReportedBy)
                   .WithMany()
                   .HasForeignKey(d => d.ReportedById)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Resolution)
                   .WithOne(r => r.Dispute)
                   .HasForeignKey<DisputeResolution>(r => r.DisputeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.Evidences)
                   .WithOne(e => e.Dispute)
                   .HasForeignKey(e => e.DisputeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

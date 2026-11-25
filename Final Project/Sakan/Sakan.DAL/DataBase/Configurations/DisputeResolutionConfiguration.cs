using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class DisputeResolutionConfiguration : IEntityTypeConfiguration<DisputeResolution>
    {
        public void Configure(EntityTypeBuilder<DisputeResolution> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.AdminId)
                   .IsRequired();

            builder.Property(r => r.Decision)
                   .IsRequired();

            builder.Property(r => r.GuestRefundAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(r => r.HostReleaseAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(r => r.DepositCapturedAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(r => r.AdminNotes)
                   .IsRequired();

            builder.Property(r => r.ResolvedAt)
                   .IsRequired();

            builder.HasOne(r => r.Admin)
                   .WithMany()
                   .HasForeignKey(r => r.AdminId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

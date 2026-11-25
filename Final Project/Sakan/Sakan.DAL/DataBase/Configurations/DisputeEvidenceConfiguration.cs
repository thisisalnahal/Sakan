using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class DisputeEvidenceConfiguration : IEntityTypeConfiguration<DisputeEvidence>
    {
        public void Configure(EntityTypeBuilder<DisputeEvidence> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UploadedById)
                   .IsRequired();

            builder.Property(e => e.StorageKey)
                   .IsRequired();

            builder.Property(e => e.FileName)
                   .IsRequired();

            builder.Property(e => e.ContentType)
                   .IsRequired();

            builder.Property(e => e.FileSizeBytes)
                   .IsRequired();

            builder.Property(e => e.UploadedAt)
                   .IsRequired();

            builder.HasOne(e => e.UploadedBy)
                   .WithMany()
                   .HasForeignKey(e => e.UploadedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

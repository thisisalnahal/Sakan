using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.Property(a => a.Action).HasMaxLength(100).IsRequired();
            builder.Property(a => a.EntityType).HasMaxLength(100).IsRequired();
            builder.Property(a => a.EntityId).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Details).HasMaxLength(2000);
            builder.Property(a => a.At).HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasOne(a => a.Actor)
                .WithMany()
                .HasForeignKey(a => a.ActorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => new { a.EntityType, a.EntityId });
            builder.HasIndex(a => new { a.ActorId, a.At });
        }
    }
}

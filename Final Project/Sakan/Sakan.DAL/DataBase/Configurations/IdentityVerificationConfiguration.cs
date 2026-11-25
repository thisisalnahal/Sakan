using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class IdentityVerificationConfiguration : IEntityTypeConfiguration<IdentityVerification>
    {
        public void Configure(EntityTypeBuilder<IdentityVerification> builder)
        {
            builder.Property(v => v.DocumentType).HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(v => v.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(v => v.FrontImageKey).HasMaxLength(300).IsRequired();
            builder.Property(v => v.BackImageKey).HasMaxLength(300);
            builder.Property(v => v.RejectionReason).HasMaxLength(500);
            builder.Property(v => v.SubmittedAt).HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.ReviewedBy)
                .WithMany()
                .HasForeignKey(v => v.ReviewedById)
                .OnDelete(DeleteBehavior.Restrict);

            // At most one Pending verification per user.
            builder.HasIndex(v => v.UserId)
                .IsUnique()
                .HasFilter("[Status] = 'Pending'")
                .HasDatabaseName("UX_IdentityVerifications_User_Pending");

            builder.HasIndex(v => new { v.Status, v.SubmittedAt });

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_IdentityVerifications_DocumentType", "[DocumentType] IN ('NationalId','Passport')");
                t.HasCheckConstraint("CK_IdentityVerifications_Status", "[Status] IN ('Pending','Approved','Rejected')");
            });
        }
    }
}

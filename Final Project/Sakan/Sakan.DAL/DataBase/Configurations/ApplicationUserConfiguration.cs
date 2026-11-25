using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.ID);

            builder.Property(u => u.FullName).HasMaxLength(150).IsRequired();
            builder.Property(u => u.DisplayName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
            builder.Property(u => u.PhoneNumber).HasMaxLength(20);
            builder.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();
            builder.Property(u => u.Gender).HasConversion<string>().HasMaxLength(10);
            builder.Property(u => u.ProfileImageKey).HasMaxLength(300);
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.IsActive);

            // Self references (all optional, Restrict).
            builder.HasOne(u => u.IdentityVerifiedBy)
                .WithMany()
                .HasForeignKey("IdentityVerifiedById")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(u => u.CreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(u => u.ModifiedBy).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(u => u.DeletedBy).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Users_Gender", "[Gender] IS NULL OR [Gender] IN ('Male','Female')"));
        }
    }
}

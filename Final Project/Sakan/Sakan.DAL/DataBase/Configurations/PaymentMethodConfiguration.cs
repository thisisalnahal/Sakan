using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.Property(p => p.MethodType).HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(p => p.Balance).HasPrecision(18, 2);

            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // At most one default payment method per user.
            builder.HasIndex(p => p.UserId)
                .IsUnique()
                .HasFilter("[IsDefault] = 1")
                .HasDatabaseName("UX_PaymentMethods_User_Default");

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_PaymentMethods_MethodType", "[MethodType] IN ('Card','Wallet')");
                t.HasCheckConstraint("CK_PaymentMethods_ExpiryMonth", "[ExpiryMonth] IS NULL OR [ExpiryMonth] BETWEEN 1 AND 12");
                t.HasCheckConstraint("CK_PaymentMethods_Balance", "[Balance] >= 0");
            });
        }
    }
}

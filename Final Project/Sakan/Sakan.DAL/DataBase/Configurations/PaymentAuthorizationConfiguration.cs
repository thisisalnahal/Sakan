using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;
using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class PaymentAuthorizationConfiguration : IEntityTypeConfiguration<PaymentAuthorization>
    {
        public void Configure(EntityTypeBuilder<PaymentAuthorization> builder)
        {
            builder.ToTable("PaymentAuthorizations", t =>
            {
                t.HasCheckConstraint("CK_PaymentAuthorization_Amount", "[Amount] >= 0");
                t.HasCheckConstraint("CK_PaymentAuthorization_Status", EnumCheck.In<PaymentAuthorizationStatus>("Status"));
            });
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).HasPrecision(18, 2);
            builder.Property(x => x.Currency).HasMaxLength(3).IsFixedLength().IsUnicode(false).IsRequired();
            builder.Property(x => x.IdempotencyKey).HasMaxLength(200).IsRequired();

            //to make the columns of enums type to be stored in the db with the string value 
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);

            builder.HasIndex(x => x.BookingId).IsUnique();
            builder.HasIndex(x => x.IdempotencyKey).IsUnique();

            builder.HasOne(x => x.Booking).WithOne().HasForeignKey<PaymentAuthorization>(x => x.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.PaymentMethod).WithMany().HasForeignKey(x => x.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

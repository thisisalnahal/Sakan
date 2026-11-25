using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;
using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class HostPayoutConfiguration : IEntityTypeConfiguration<HostPayout>
    {
        public void Configure(EntityTypeBuilder<HostPayout> builder)
        {
            builder.ToTable("HostPayouts", t =>
            {
                t.HasCheckConstraint("CK_HostPayout_Amounts",
                    "[GrossAmount] >= 0 AND [CommissionAmount] >= 0 AND [PenaltyDeductionAmount] >= 0 AND [NetAmount] >= 0");
                t.HasCheckConstraint("CK_HostPayout_Net",
                    "[NetAmount] = [GrossAmount] - [CommissionAmount] - [PenaltyDeductionAmount]");
                t.HasCheckConstraint("CK_HostPayout_Type", EnumCheck.In<PayoutType>("PayoutType"));
                t.HasCheckConstraint("CK_HostPayout_Status", EnumCheck.In<PayoutStatus>("Status"));
            });
            builder.HasKey(x => x.Id);
            builder.Property(x => x.GrossAmount).HasPrecision(18, 2);
            builder.Property(x => x.CommissionAmount).HasPrecision(18, 2);
            builder.Property(x => x.PenaltyDeductionAmount).HasPrecision(18, 2);
            builder.Property(x => x.NetAmount).HasPrecision(18, 2);
            builder.Property(x => x.Currency).HasMaxLength(3).IsFixedLength().IsUnicode(false).IsRequired();

            //to make the columns of enums type to be stored in the db with the string value 
            builder.Property(x => x.PayoutType).HasConversion<string>().HasMaxLength(30);
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);

            builder.HasIndex(x => new { x.BookingId, x.PayoutType }).IsUnique();
            builder.HasIndex(x => new { x.HostId, x.Status });
            builder.HasIndex(x => new { x.Status, x.ScheduledAt });

            builder.HasOne(x => x.Booking).WithMany().HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Host).WithMany().HasForeignKey(x => x.HostId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

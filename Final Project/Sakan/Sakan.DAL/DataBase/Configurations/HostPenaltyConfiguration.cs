using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;
using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class HostPenaltyConfiguration : IEntityTypeConfiguration<HostPenalty>
    {
        public void Configure(EntityTypeBuilder<HostPenalty> builder)
        {
            builder.ToTable("HostPenalties", t =>
            {
                t.HasCheckConstraint("CK_HostPenalty_Amount", "[Amount] >= 0 AND [RemainingAmount] >= 0");
                t.HasCheckConstraint("CK_HostPenalty_Remaining", "[RemainingAmount] <= [Amount]");
                t.HasCheckConstraint("CK_HostPenalty_Status", EnumCheck.In<HostPenaltyStatus>("Status"));
            });
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).HasPrecision(18, 2);
            builder.Property(x => x.RemainingAmount).HasPrecision(18, 2);

            //to make the columns of enums type to be stored in the db with the string value 
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);

            builder.HasIndex(x => x.CancelledBookingId).IsUnique();

            builder.HasOne(x => x.Host).WithMany().HasForeignKey(x => x.HostId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.CancelledBooking).WithOne().HasForeignKey<HostPenalty>(x => x.CancelledBookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

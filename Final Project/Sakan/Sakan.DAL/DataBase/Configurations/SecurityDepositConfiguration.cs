using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;
using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class SecurityDepositConfiguration : IEntityTypeConfiguration<SecurityDeposit>
    {
        public void Configure(EntityTypeBuilder<SecurityDeposit> builder)
        {
            builder.ToTable("SecurityDeposits", t =>
            {
                t.HasCheckConstraint("CK_SecurityDeposit_Amount", "[Amount] >= 0");
                t.HasCheckConstraint("CK_SecurityDeposit_Captured", "[CapturedAmount] >= 0 AND [CapturedAmount] <= [Amount]");
                t.HasCheckConstraint("CK_SecurityDeposit_Status", EnumCheck.In<SecurityDepositStatus>("Status"));
            });
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).HasPrecision(18, 2);
            builder.Property(x => x.CapturedAmount).HasPrecision(18, 2);

            //to make the columns of enums type to be stored in the db with the string value 
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);

            builder.HasIndex(x => x.BookingId).IsUnique();

            builder.HasOne(x => x.Booking).WithOne().HasForeignKey<SecurityDeposit>(x => x.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

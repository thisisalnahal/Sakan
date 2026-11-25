using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;
using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class FinancialTransactionConfiguration : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {
            const string hostSide = "('Payout','PlatformFee','PenaltyDeduction')";
            builder.ToTable("FinancialTransactions", t =>
            {
                t.HasCheckConstraint("CK_FinancialTransaction_Amount", "[Amount] > 0");
                t.HasCheckConstraint("CK_FinancialTransaction_Type", EnumCheck.In<TransactionType>("TransactionType"));
                t.HasCheckConstraint("CK_FinancialTransaction_Status", EnumCheck.In<TransactionStatus>("Status"));
                // Exactly one reference depending on the type (XOR)
                t.HasCheckConstraint("CK_FinancialTransaction_Xor",
                    $"([TransactionType] IN {hostSide} AND [HostPayoutId] IS NOT NULL AND [PaymentAuthorizationId] IS NULL) " +
                    $"OR ([TransactionType] NOT IN {hostSide} AND [HostPayoutId] IS NULL AND [PaymentAuthorizationId] IS NOT NULL)");
            });
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).HasPrecision(18, 2);
            builder.Property(x => x.Currency).HasMaxLength(3).IsFixedLength().IsUnicode(false).IsRequired();
            builder.Property(x => x.IdempotencyKey).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ExternalReference).HasMaxLength(200);
            builder.Property(x => x.Reason).HasMaxLength(500);

            //to make the columns of enums type to be stored in the db with the string value 
            builder.Property(x => x.TransactionType).HasConversion<string>().HasMaxLength(30);
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);

            builder.HasIndex(x => x.IdempotencyKey).IsUnique();
            builder.HasIndex(x => x.BookingId);

            builder.HasOne(x => x.Booking).WithMany().HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.PaymentAuthorization).WithMany(a => a.Transactions)
                .HasForeignKey(x => x.PaymentAuthorizationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.HostPayout).WithMany(p => p.Transactions)
                .HasForeignKey(x => x.HostPayoutId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.DisputeResolution).WithMany()
                .HasForeignKey(x => x.DisputeResolutionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.InitiatedBy).WithMany()
                .HasForeignKey(x => x.InitiatedById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

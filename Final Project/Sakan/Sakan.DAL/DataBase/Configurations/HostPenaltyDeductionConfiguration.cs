using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.DataBase.Configurations
{
    internal class HostPenaltyDeductionConfiguration : IEntityTypeConfiguration<HostPenaltyDeduction>
    {
        public void Configure(EntityTypeBuilder<HostPenaltyDeduction> builder)
        {
            builder.ToTable("HostPenaltyDeductions", t =>
           t.HasCheckConstraint("CK_HostPenaltyDeduction_Amount", "[Amount] >= 0"));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).HasPrecision(18, 2);

            builder.HasIndex(x => new { x.HostPenaltyId, x.HostPayoutId }).IsUnique();

            builder.HasOne(x => x.HostPenalty).WithMany(p => p.Deductions)
                .HasForeignKey(x => x.HostPenaltyId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.HostPayout).WithMany(p => p.PenaltyDeductions)
                .HasForeignKey(x => x.HostPayoutId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

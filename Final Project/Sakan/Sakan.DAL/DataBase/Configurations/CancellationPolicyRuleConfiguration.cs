using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.Configurations;

public class CancellationPolicyRuleConfiguration : IEntityTypeConfiguration<CancellationPolicyRule>
{
    public void Configure(EntityTypeBuilder<CancellationPolicyRule> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.RefundPercentage).HasPrecision(5, 2);
        b.ToTable(t =>
        {
            t.HasCheckConstraint("CK_CancellationPolicyRule_Refund", "[RefundPercentage] BETWEEN 0 AND 100");
            t.HasCheckConstraint("CK_CancellationPolicyRule_Hours", "[MinHoursBeforeCheckIn] >= 0");
        });

        b.HasOne(x => x.CancellationPolicy).WithMany(x => x.CancellationPolicyRules)
            .HasForeignKey(x => x.PolicyId).OnDelete(DeleteBehavior.Cascade);
    }
}


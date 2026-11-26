using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.Configurations;

public class SharedSpaceConfiguration : IEntityTypeConfiguration<SharedSpace>
{
    public void Configure(EntityTypeBuilder<SharedSpace> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired().HasMaxLength(100);
        b.Property(x => x.Description).HasMaxLength(1000);
        b.Property(x => x.SpaceType).HasConversion<string>().HasMaxLength(20);
        b.ToTable(t => t.HasCheckConstraint("CK_SharedSpace_SpaceType",
            "[SpaceType] IN ('Kitchen','LivingRoom','Bathroom','Balcony','Other')"));

        b.HasOne(x => x.Property).WithMany(x => x.SharedSpaces)
            .HasForeignKey(x => x.PropertyId).OnDelete(DeleteBehavior.Cascade);
    }
} 


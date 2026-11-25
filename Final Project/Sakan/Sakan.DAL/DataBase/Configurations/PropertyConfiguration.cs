using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Title).IsRequired().HasMaxLength(200);
        b.Property(x => x.Description).IsRequired().HasMaxLength(4000);
        b.Property(x => x.PropertyType).HasConversion<string>().HasMaxLength(20);
        b.Property(x => x.RentalMode).HasConversion<string>().HasMaxLength(20);
        b.Property(x => x.GenderPreference).HasConversion<string>().HasMaxLength(20);

        b.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Property_PropertyType",
                "[PropertyType] IN ('Apartment','House','Villa','Studio','Hotel','Other')");
            t.HasCheckConstraint("CK_Property_RentalMode",
                "[RentalMode] IN ('EntireProperty','SeparateRooms')");
            t.HasCheckConstraint("CK_Property_GenderPreference",
                "[GenderPreference] IS NULL OR [GenderPreference] IN ('MaleOnly','FemaleOnly')");
            t.HasCheckConstraint("CK_Property_Hotel_SeparateRooms",
                "[PropertyType] <> 'Hotel' OR [RentalMode] = 'SeparateRooms'");
        });

        b.HasOne(x => x.Host).WithMany().HasForeignKey(x => x.HostId).OnDelete(DeleteBehavior.Restrict);
        b.HasOne(x => x.PropertyAddress).WithOne(x => x.Property)
            .HasForeignKey<PropertyAddress>(x => x.PropertyId).OnDelete(DeleteBehavior.Cascade);
    }
}


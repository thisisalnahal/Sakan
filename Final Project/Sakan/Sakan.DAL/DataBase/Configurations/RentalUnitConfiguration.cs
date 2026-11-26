using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.Configurations;

public class RentalUnitConfiguration : IEntityTypeConfiguration<RentalUnit>
{
    public void Configure(EntityTypeBuilder<RentalUnit> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired().HasMaxLength(100);
        b.Property(x => x.Description).IsRequired().HasMaxLength(2000);
        b.Property(x => x.AreaSqm).HasPrecision(18, 2);
        b.Property(x => x.UnitType).HasConversion<string>().HasMaxLength(20);
        b.Property(x => x.FurnishingStatus).HasConversion<string>().HasMaxLength(20);

        b.ToTable(t =>
        {
            t.HasCheckConstraint("CK_RentalUnit_UnitType",
                "[UnitType] IN ('EntireProperty','PrivateRoom','HotelRoom')");
            t.HasCheckConstraint("CK_RentalUnit_FurnishingStatus",
                "[FurnishingStatus] IN ('Furnished','SemiFurnished','Unfurnished')");
            t.HasCheckConstraint("CK_RentalUnit_MaxGuests", "[MaxGuests] > 0");
        });

        // One EntireProperty unit per property
        b.HasIndex(x => x.PropertyId).IsUnique().HasFilter("[UnitType] = 'EntireProperty'");

        b.HasOne(x => x.Property).WithMany(x => x.RentalUnits)
            .HasForeignKey(x => x.PropertyId).OnDelete(DeleteBehavior.Restrict);
    }
}


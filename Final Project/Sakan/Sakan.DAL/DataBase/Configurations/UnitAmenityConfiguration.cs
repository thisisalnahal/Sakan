using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.Configurations;

public class UnitAmenityConfiguration : IEntityTypeConfiguration<UnitAmenity>
{
    public void Configure(EntityTypeBuilder<UnitAmenity> b)
    {
        b.HasKey(x => new { x.RentalUnitId, x.AmenityId });
        b.HasOne(x => x.RentalUnit).WithMany(x => x.UnitAmenities)
            .HasForeignKey(x => x.RentalUnitId).OnDelete(DeleteBehavior.Cascade);
        b.HasOne(x => x.Amenity).WithMany(x => x.UnitAmenities)
            .HasForeignKey(x => x.AmenityId).OnDelete(DeleteBehavior.Restrict);
    }
}


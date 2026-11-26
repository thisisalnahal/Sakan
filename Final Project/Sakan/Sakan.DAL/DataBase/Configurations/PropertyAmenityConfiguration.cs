using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.Configurations;

public class PropertyAmenityConfiguration : IEntityTypeConfiguration<PropertyAmenity>
{
    public void Configure(EntityTypeBuilder<PropertyAmenity> b)
    {
        b.HasKey(x => new { x.PropertyId, x.AmenityId });
        b.HasOne(x => x.Property).WithMany(x => x.PropertyAmenities)
            .HasForeignKey(x => x.PropertyId).OnDelete(DeleteBehavior.Cascade);
        b.HasOne(x => x.Amenity).WithMany(x => x.PropertyAmenities)
            .HasForeignKey(x => x.AmenityId).OnDelete(DeleteBehavior.Restrict);
    }
}


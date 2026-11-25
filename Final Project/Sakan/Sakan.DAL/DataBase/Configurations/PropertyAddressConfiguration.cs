using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakan.DAL.Entities;

namespace Sakan.DAL.Configurations;

public class PropertyAddressConfiguration : IEntityTypeConfiguration<PropertyAddress>
{
    public void Configure(EntityTypeBuilder<PropertyAddress> b)
    {
        b.HasKey(x => x.PropertyId);
        b.Property(x => x.Country).IsRequired().HasMaxLength(100);
        b.Property(x => x.City).IsRequired().HasMaxLength(100);
        b.Property(x => x.Governorate).HasMaxLength(100);
        b.Property(x => x.District).HasMaxLength(100);
        b.Property(x => x.Street).HasMaxLength(200);
        b.Property(x => x.BuildingNumber).HasMaxLength(20);
        b.Property(x => x.PostalCode).HasMaxLength(20);
        b.Property(x => x.ApartmentNumber).HasMaxLength(20);
        b.Property(x => x.AdditionalDetails).HasMaxLength(500);
        b.Property(x => x.Latitude).HasPrecision(9, 6);
        b.Property(x => x.Longitude).HasPrecision(9, 6);
    }
}


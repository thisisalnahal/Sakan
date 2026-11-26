using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    public class PropertyAmenityRepository : IPropertyAmenityRepository
    {
        private readonly ApplicationDbContext Db;

        internal PropertyAmenityRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(PropertyAmenity propertyAmenity)
        {
            try
            {
                bool propertyExists = Db.Properties.Any(p => p.Id == propertyAmenity.PropertyId);
                if (!propertyExists)
                    return (true, "Property not found");

                bool amenityExists = Db.Amenities.Any(a => a.Id == propertyAmenity.AmenityId);
                if (!amenityExists)
                    return (true, "Amenity not found");

                bool exists = Db.PropertyAmenities.Any(pa =>
                    pa.PropertyId == propertyAmenity.PropertyId &&
                    pa.AmenityId == propertyAmenity.AmenityId);

                if (exists)
                    return (true, "Amenity is already assigned to this property");

                Db.PropertyAmenities.Add(propertyAmenity);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<PropertyAmenity>?) GetAll()
        {
            try
            {
                var list = Db.PropertyAmenities
                    .Include(pa => pa.Property)
                    .Include(pa => pa.Amenity)
                    .ToList();

                return (false, null, list);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, List<PropertyAmenity>?) GetByPropertyId(int propertyId)
        {
            try
            {
                var list = Db.PropertyAmenities
                    .Include(pa => pa.Amenity)
                    .Where(pa => pa.PropertyId == propertyId)
                    .ToList();

                return (false, null, list);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, PropertyAmenity?) GetById(int propertyId, int amenityId)
        {
            try
            {
                var item = Db.PropertyAmenities
                    .Include(pa => pa.Property)
                    .Include(pa => pa.Amenity)
                    .FirstOrDefault(pa => pa.PropertyId == propertyId && pa.AmenityId == amenityId);

                if (item == null)
                    return (true, "PropertyAmenity not found", null);

                return (false, null, item);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int propertyId, int amenityId)
        {
            try
            {
                var item = Db.PropertyAmenities
                    .FirstOrDefault(pa => pa.PropertyId == propertyId && pa.AmenityId == amenityId);

                if (item == null)
                    return (true, "PropertyAmenity not found");

                Db.PropertyAmenities.Remove(item);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }
    }
}

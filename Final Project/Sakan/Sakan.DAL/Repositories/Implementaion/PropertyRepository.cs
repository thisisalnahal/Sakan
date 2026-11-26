using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Enums.Booking;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext Db;

        internal PropertyRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(Property property)
        {
            try
            {
                property.CreatedAt = DateTime.UtcNow;
                property.UpdatedAt = DateTime.UtcNow;

                Db.Properties.Add(property);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(Property newProperty, int oldPropertyId)
        {
            try
            {
                var oldProperty = Db.Properties.FirstOrDefault(p => p.Id == oldPropertyId);
                if (oldProperty == null)
                    return (true, "Property not found");

                if (oldProperty.GenderPreference != newProperty.GenderPreference)
                {
                    var activeStatuses = new[] { BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.CheckedIn };
                    bool hasActiveBookings = Db.Bookings.Any(b =>
                        b.Listing.RentalUnit.PropertyId == oldPropertyId &&
                        activeStatuses.Contains(b.Status));

                    if (hasActiveBookings)
                        return (true, "Cannot change GenderPreference while there are active bookings");
                }

                oldProperty.Title = newProperty.Title;
                oldProperty.Description = newProperty.Description;
                oldProperty.PropertyType = newProperty.PropertyType;
                oldProperty.RentalMode = newProperty.RentalMode;
                oldProperty.GenderPreference = newProperty.GenderPreference;
                oldProperty.IsActive = newProperty.IsActive;
                oldProperty.UpdatedAt = DateTime.UtcNow;

                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<Property>?) GetAll()
        {
            try
            {
                var properties = Db.Properties
                    .Include(p => p.PropertyAddress)
                    .Include(p => p.Host)
                    .ToList();

                return (false, null, properties);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, Property?) GetById(int propertyId)
        {
            try
            {
                var property = Db.Properties
                    .Include(p => p.Host)
                    .Include(p => p.PropertyAddress)
                    .Include(p => p.RentalUnits)
                    .Include(p => p.SharedSpaces)
                    .Include(p => p.PropertyAmenities)
                        .ThenInclude(pa => pa.Amenity)
                    .FirstOrDefault(p => p.Id == propertyId);

                if (property == null)
                    return (true, "Property not found", null);

                return (false, null, property);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int propertyId)
        {
            try
            {
                var property = Db.Properties.FirstOrDefault(p => p.Id == propertyId);
                if (property == null)
                    return (true, "Property not found");

                Db.Properties.Remove(property);
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

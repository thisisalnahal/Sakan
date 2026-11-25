using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    public class RentalUnitRepository : IRentalUnitRepository
    {
        private readonly ApplicationDbContext Db;

        public RentalUnitRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(RentalUnit rentalUnit)
        {
            try
            {
                var property = Db.Properties.FirstOrDefault(p => p.Id == rentalUnit.PropertyId);
                if (property == null)
                    return (true, "Property not found");

                if (rentalUnit.MaxGuests <= 0)
                    return (true, "MaxGuests must be greater than 0");

                if (property.RentalMode == RentalMode.SeparateRooms &&
                    rentalUnit.UnitType == UnitType.EntireProperty)
                    return (true, "Cannot add EntireProperty unit to a property with SeparateRooms mode");

                Db.RentalUnits.Add(rentalUnit);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(RentalUnit newRentalUnit, int oldRentalUnitId)
        {
            try
            {
                var oldUnit = Db.RentalUnits.FirstOrDefault(u => u.Id == oldRentalUnitId);
                if (oldUnit == null)
                    return (true, "RentalUnit not found");

                if (newRentalUnit.MaxGuests <= 0)
                    return (true, "MaxGuests must be greater than 0");

                var property = Db.Properties.FirstOrDefault(p => p.Id == oldUnit.PropertyId);
                if (property != null &&
                    property.RentalMode == RentalMode.SeparateRooms &&
                    newRentalUnit.UnitType == UnitType.EntireProperty)
                    return (true, "Cannot set UnitType to EntireProperty when Property RentalMode is SeparateRooms");

                oldUnit.Name = newRentalUnit.Name;
                oldUnit.Description = newRentalUnit.Description;
                oldUnit.MaxGuests = newRentalUnit.MaxGuests;
                oldUnit.Bedrooms = newRentalUnit.Bedrooms;
                oldUnit.Bathrooms = newRentalUnit.Bathrooms;
                oldUnit.AreaSqm = newRentalUnit.AreaSqm;
                oldUnit.UnitType = newRentalUnit.UnitType;
                oldUnit.FurnishingStatus = newRentalUnit.FurnishingStatus;
                oldUnit.IsActive = newRentalUnit.IsActive;

                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<RentalUnit>?) GetAll()
        {
            try
            {
                var units = Db.RentalUnits
                    .Include(u => u.Property)
                    .ToList();

                return (false, null, units);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, RentalUnit?) GetById(int rentalUnitId)
        {
            try
            {
                var unit = Db.RentalUnits
                    .Include(u => u.Property)
                    .Include(u => u.UnitAmenities)
                        .ThenInclude(ua => ua.Amenity)
                    .FirstOrDefault(u => u.Id == rentalUnitId);

                if (unit == null)
                    return (true, "RentalUnit not found", null);

                return (false, null, unit);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int rentalUnitId)
        {
            try
            {
                var unit = Db.RentalUnits.FirstOrDefault(u => u.Id == rentalUnitId);
                if (unit == null)
                    return (true, "RentalUnit not found");

                Db.RentalUnits.Remove(unit);
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

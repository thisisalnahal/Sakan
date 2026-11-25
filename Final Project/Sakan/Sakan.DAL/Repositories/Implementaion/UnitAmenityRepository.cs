using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    public class UnitAmenityRepository : IUnitAmenityRepository
    {
        private readonly ApplicationDbContext Db;

        public UnitAmenityRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(UnitAmenity unitAmenity)
        {
            try
            {
                bool unitExists = Db.RentalUnits.Any(u => u.Id == unitAmenity.RentalUnitId);
                if (!unitExists)
                    return (true, "RentalUnit not found");

                bool amenityExists = Db.Amenities.Any(a => a.Id == unitAmenity.AmenityId);
                if (!amenityExists)
                    return (true, "Amenity not found");

                bool exists = Db.UnitAmenities.Any(ua =>
                    ua.RentalUnitId == unitAmenity.RentalUnitId &&
                    ua.AmenityId == unitAmenity.AmenityId);

                if (exists)
                    return (true, "Amenity is already assigned to this rental unit");

                Db.UnitAmenities.Add(unitAmenity);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<UnitAmenity>?) GetAll()
        {
            try
            {
                var list = Db.UnitAmenities
                    .Include(ua => ua.RentalUnit)
                    .Include(ua => ua.Amenity)
                    .ToList();

                return (false, null, list);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, List<UnitAmenity>?) GetByRentalUnitId(int rentalUnitId)
        {
            try
            {
                var list = Db.UnitAmenities
                    .Include(ua => ua.Amenity)
                    .Where(ua => ua.RentalUnitId == rentalUnitId)
                    .ToList();

                return (false, null, list);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, UnitAmenity?) GetById(int rentalUnitId, int amenityId)
        {
            try
            {
                var item = Db.UnitAmenities
                    .Include(ua => ua.RentalUnit)
                    .Include(ua => ua.Amenity)
                    .FirstOrDefault(ua => ua.RentalUnitId == rentalUnitId && ua.AmenityId == amenityId);

                if (item == null)
                    return (true, "UnitAmenity not found", null);

                return (false, null, item);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int rentalUnitId, int amenityId)
        {
            try
            {
                var item = Db.UnitAmenities
                    .FirstOrDefault(ua => ua.RentalUnitId == rentalUnitId && ua.AmenityId == amenityId);

                if (item == null)
                    return (true, "UnitAmenity not found");

                Db.UnitAmenities.Remove(item);
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

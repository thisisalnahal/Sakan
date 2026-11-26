using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Sakan.DAL.DataBase;

namespace Sakan.DAL.Repositories.Implementaion
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly ApplicationDbContext Db;

        internal AmenityRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(Amenity amenity)
        {
            try
            {
                Db.Amenities.Add(amenity);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(Amenity newAmenity, int oldAmenityId)
        {
            try
            {
                var oldAmenity = Db.Amenities.FirstOrDefault(a => a.Id == oldAmenityId);
                if (oldAmenity == null)
                    return (true, "Amenity not found");

                oldAmenity.Name = newAmenity.Name;
                oldAmenity.Category = newAmenity.Category;
                oldAmenity.Icon = newAmenity.Icon;
                oldAmenity.IsActive = newAmenity.IsActive;

                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<Amenity>?) GetAll()
        {
            try
            {
                var amenities = Db.Amenities.ToList();
                return (false, null, amenities);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, Amenity?) GetById(int amenityId)
        {
            try
            {
                var amenity = Db.Amenities.FirstOrDefault(a => a.Id == amenityId);
                if (amenity == null)
                    return (true, "Amenity not found", null);

                return (false, null, amenity);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int amenityId)
        {
            try
            {
                var amenity = Db.Amenities.FirstOrDefault(a => a.Id == amenityId);
                if (amenity == null)
                    return (true, "Amenity not found");

                Db.Amenities.Remove(amenity);
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

using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    public class PropertyAddressRepository : IPropertyAddressRepository
    {
        private readonly ApplicationDbContext Db;

        internal PropertyAddressRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(PropertyAddress address)
        {
            try
            {
                bool propertyExists = Db.Properties.Any(p => p.Id == address.PropertyId);
                if (!propertyExists)
                    return (true, "Property not found");

                bool alreadyHasAddress = Db.PropertyAddresses.Any(a => a.PropertyId == address.PropertyId);
                if (alreadyHasAddress)
                    return (true, "This property already has an address");

                Db.PropertyAddresses.Add(address);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(PropertyAddress newAddress, int propertyId)
        {
            try
            {
                var oldAddress = Db.PropertyAddresses.FirstOrDefault(a => a.PropertyId == propertyId);
                if (oldAddress == null)
                    return (true, "PropertyAddress not found");

                oldAddress.Country = newAddress.Country;
                oldAddress.City = newAddress.City;
                oldAddress.Governorate = newAddress.Governorate;
                oldAddress.District = newAddress.District;
                oldAddress.Street = newAddress.Street;
                oldAddress.BuildingNumber = newAddress.BuildingNumber;
                oldAddress.PostalCode = newAddress.PostalCode;
                oldAddress.ApartmentNumber = newAddress.ApartmentNumber;
                oldAddress.FloorNumber = newAddress.FloorNumber;
                oldAddress.Latitude = newAddress.Latitude;
                oldAddress.Longitude = newAddress.Longitude;
                oldAddress.AdditionalDetails = newAddress.AdditionalDetails;

                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<PropertyAddress>?) GetAll()
        {
            try
            {
                var addresses = Db.PropertyAddresses
                    .Include(a => a.Property)
                    .ToList();

                return (false, null, addresses);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, PropertyAddress?) GetById(int propertyId)
        {
            try
            {
                var address = Db.PropertyAddresses
                    .Include(a => a.Property)
                    .FirstOrDefault(a => a.PropertyId == propertyId);

                if (address == null)
                    return (true, "PropertyAddress not found", null);

                return (false, null, address);
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
                var address = Db.PropertyAddresses.FirstOrDefault(a => a.PropertyId == propertyId);
                if (address == null)
                    return (true, "PropertyAddress not found");

                Db.PropertyAddresses.Remove(address);
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

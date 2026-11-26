using System;
using System.Collections.Generic;
using System.Text;

using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IUnitAmenityRepository
    {
        (bool, string?) Create(UnitAmenity unitAmenity);
        (bool, string?, List<UnitAmenity>?) GetAll();
        (bool, string?, List<UnitAmenity>?) GetByRentalUnitId(int rentalUnitId);
        (bool, string?, UnitAmenity?) GetById(int rentalUnitId, int amenityId);
        (bool, string?) Delete(int rentalUnitId, int amenityId);
    }
} 


using System;
using System.Collections.Generic;
using System.Text;

using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IPropertyAmenityRepository
    {
        (bool, string?) Create(PropertyAmenity propertyAmenity);
        (bool, string?, List<PropertyAmenity>?) GetAll();
        (bool, string?, List<PropertyAmenity>?) GetByPropertyId(int propertyId);
        (bool, string?, PropertyAmenity?) GetById(int propertyId, int amenityId);
        (bool, string?) Delete(int propertyId, int amenityId);
    }
} 

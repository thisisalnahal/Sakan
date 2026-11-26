using System;
using System.Collections.Generic;
using System.Text;

using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IPropertyAddressRepository
    {
        (bool, string?) Create(PropertyAddress address);
        (bool, string?) Edit(PropertyAddress newAddress, int propertyId);
        (bool, string?, List<PropertyAddress>?) GetAll();
        (bool, string?, PropertyAddress?) GetById(int propertyId);
        (bool, string?) Delete(int propertyId);
    }
} 


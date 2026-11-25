using System;
using System.Collections.Generic;
using System.Text;

using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IPropertyRepository
    {
        (bool, string?) Create(Property property);
        (bool, string?) Edit(Property newProperty, int oldPropertyId);
        (bool, string?, List<Property>?) GetAll();
        (bool, string?, Property?) GetById(int propertyId);
        (bool, string?) Delete(int propertyId);
    }
} 


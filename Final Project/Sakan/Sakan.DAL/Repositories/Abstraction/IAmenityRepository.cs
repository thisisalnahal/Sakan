using System;
using System.Collections.Generic;
using System.Text;

using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IAmenityRepository
    {
        (bool, string?) Create(Amenity amenity);
        (bool, string?) Edit(Amenity newAmenity, int oldAmenityId);
        (bool, string?, List<Amenity>?) GetAll();
        (bool, string?, Amenity?) GetById(int amenityId);
        (bool, string?) Delete(int amenityId);
    }
} 


using System;
using System.Collections.Generic;
using System.Text;

using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IRentalUnitRepository
    {
        (bool, string?) Create(RentalUnit rentalUnit);
        (bool, string?) Edit(RentalUnit newRentalUnit, int oldRentalUnitId);
        (bool, string?, List<RentalUnit>?) GetAll();
        (bool, string?, RentalUnit?) GetById(int rentalUnitId);
        (bool, string?) Delete(int rentalUnitId);
    }
}  


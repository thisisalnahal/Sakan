namespace Sakan.DAL.Entities;

using System.Security.AccessControl;
using Sakan.DAL.Enums.ApplicationUser;

public class UnitAmenity
{
    public int RentalUnitId { get; set; } // composite PK
    public int AmenityId { get; set; } // composite PK

    // Navigation properties
    public RentalUnit RentalUnit { get; set; } = null!;
    public Amenity Amenity { get; set; } = null!;
}


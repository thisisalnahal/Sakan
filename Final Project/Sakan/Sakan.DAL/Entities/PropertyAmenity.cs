namespace Sakan.DAL.Entities;

using System.Security.AccessControl;
using Sakan.DAL.Enums.ApplicationUser;

public class PropertyAmenity
{
    public int PropertyId { get; set; } // composite PK
    public int AmenityId { get; set; } // composite PK

    // Navigation properties
    public Property Property { get; set; } = null!;
    public Amenity Amenity { get; set; } = null!;
}


namespace Sakan.DAL.Entities;

using Sakan.DAL.Enums.ApplicationUser;
using System.Security.AccessControl;



public class Amenity 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public ICollection<PropertyAmenity> PropertyAmenities { get; set; } = new List<PropertyAmenity>();
    public ICollection<UnitAmenity> UnitAmenities { get; set; } = new List<UnitAmenity>();
}


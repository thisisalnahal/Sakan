namespace Sakan.DAL.Entities;

using Sakan.DAL.Enums.ApplicationUser;

public class Property
{
    public int Id { get; set; }
    public string HostId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public RentalMode RentalMode { get; set; }
    public GenderPreference? GenderPreference { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public ApplicationUser Host { get; set; } = null!;
    public PropertyAddress? PropertyAddress { get; set; }
    public ICollection<RentalUnit> RentalUnits { get; set; } = new List<RentalUnit>();
    public ICollection<SharedSpace> SharedSpaces { get; set; } = new List<SharedSpace>();
    public ICollection<PropertyAmenity> PropertyAmenities { get; set; } = new List<PropertyAmenity>();
}

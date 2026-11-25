using System.Reflection;
namespace Sakan.DAL.Entities;

using Sakan.DAL.Enums.ApplicationUser;

public class RentalUnit
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public UnitType UnitType { get; set; }
    public string Description { get; set; } = string.Empty;
    public int MaxGuests { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public decimal? AreaSqm { get; set; }
    public FurnishingStatus FurnishingStatus { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public Property Property { get; set; } = null!;
    public Listing? Listing { get; set; }
    public ICollection<UnitAmenity> UnitAmenities { get; set; } = new List<UnitAmenity>();
    public ICollection<AvailabilityBlock> AvailabilityBlocks { get; set; } = new List<AvailabilityBlock>();
}

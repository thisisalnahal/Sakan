namespace Sakan.DAL.Entities;

using Sakan.DAL.Enums.ApplicationUser;
public class PropertyAddress
{
    public int PropertyId { get; set; } // PK + FK (shared PK)
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? Governorate { get; set; } // required for Egypt (app validation)
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? BuildingNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? ApartmentNumber { get; set; }
    public int? FloorNumber { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? AdditionalDetails { get; set; }

    // Navigation properties
    public Property Property { get; set; } = null!;
}

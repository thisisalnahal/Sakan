using Sakan.DAL.Enums.ApplicationUser;
using System.Security.AccessControl;
namespace Sakan.DAL.Entities;



public class SharedSpace
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public SpaceType SpaceType { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public Property Property { get; set; } = null!;
}


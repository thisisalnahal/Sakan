

namespace Sakan.DAL.Entities;

using System.Security.AccessControl;
using Sakan.DAL.Enums.ApplicationUser;

public class CancellationPolicy
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    // Navigation properties
    public ICollection<CancellationPolicyRule> CancellationPolicyRules { get; set; } = new List<CancellationPolicyRule>();
    public ICollection<Listing> Listings { get; set; } = new List<Listing>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}


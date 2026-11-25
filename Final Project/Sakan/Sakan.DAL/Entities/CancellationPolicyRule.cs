namespace Sakan.DAL.Entities;

using System.Security.AccessControl;
using Sakan.DAL.Enums.ApplicationUser;

public class CancellationPolicyRule
{
    public int Id { get; set; }
    public int PolicyId { get; set; }
    public int MinHoursBeforeCheckIn { get; set; }
    public decimal RefundPercentage { get; set; }

    // Navigation properties
    public CancellationPolicy CancellationPolicy { get; set; } = null!;
}


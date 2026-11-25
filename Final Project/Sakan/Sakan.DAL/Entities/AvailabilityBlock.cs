using Sakan.DAL.Enums.AvailabilityBlock;

namespace Sakan.DAL.Entities
{
    public class AvailabilityBlock
    {
        public int Id { get; set; }
        public int RentalUnitId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BlockType BlockType { get; set; }
        public string? Reason { get; set; }
        public string? CreatedById { get; set; }
        public int? BookingId { get; set; }
        public int? DisputeResolutionId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public RentalUnit RentalUnit { get; set; } = null!;
        public ApplicationUser? CreatedBy { get; set; }
        public Booking? Booking { get; set; }
        public DisputeResolution? DisputeResolution { get; set; }
    }
}
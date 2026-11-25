using Sakan.DAL.Enums.Dispute;

namespace Sakan.DAL.Entities
{
    internal class Dispute
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DisputeType DisputeType { get; set; }
        public string ReportedById { get; set; } 
        public DisputeCategory Category { get; set; }
        public string Description { get; set; } 
        public decimal? ClaimedAmount { get; set; }
        public string? CounterpartyResponse { get; set; }
        public DateTime? RespondedAt { get; set; }
        public DisputeStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? ReviewDueAt { get; set; }

        // Navigation Properties
        public Booking Booking { get; set; } 
        public ApplicationUser ReportedBy { get; set; } 
        public DisputeResolution? Resolution { get; set; }
        public ICollection<DisputeEvidence> Evidences { get; set; } = new List<DisputeEvidence>();
    }
}

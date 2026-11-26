namespace Sakan.DAL.Entities
{
    public class DisputeResolution
    {
        public int Id { get; set; }
        public int DisputeId { get; set; }
        public string AdminId { get; set; } 
        public string Decision { get; set; }  
        public decimal GuestRefundAmount { get; set; }
        public decimal HostReleaseAmount { get; set; }
        public decimal DepositCapturedAmount { get; set; }
        public string AdminNotes { get; set; } 
        public DateTime ResolvedAt { get; set; } 

        // Navigation Properties
        public Dispute Dispute { get; set; }
        public ApplicationUser Admin { get; set; } 
    }
}

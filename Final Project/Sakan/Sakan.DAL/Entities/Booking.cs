using Microsoft.VisualBasic;
using Sakan.DAL.Enums.Booking;

namespace Sakan.DAL.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public string GuestId { get; set; } = null!;
        public int CancellationPolicyId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfGuests { get; set; }
        public string? PricingBreakdown { get; set; }   // json
        public decimal RentalAmount { get; set; }
        public decimal SecurityDepositAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CommissionRate { get; set; }
        public string Currency { get; set; } = null!;   // char(3)
        public BookingStatus Status { get; set; }
        public bool HostApprovalRequired { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? IssueWindowEndsAt { get; set; }
        public DateTime? DamageWindowEndsAt { get; set; }
        public string? CancelledById { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }

        // Navigation Properties
        public Listing Listing { get; set; } = null!;
        public ApplicationUser Guest { get; set; } = null!;
        public CancellationPolicy CancellationPolicy { get; set; } = null!;
        public ApplicationUser? CancelledBy { get; set; }
        public ICollection<BookingStatusHistory> StatusHistory { get; set; } = new List<BookingStatusHistory>();
        public PaymentAuthorization? PaymentAuthorization { get; set; }
        public SecurityDeposit? SecurityDeposit { get; set; }
        public ICollection<FinancialTransaction> FinancialTransactions { get; set; } = new List<FinancialTransaction>();
        public ICollection<HostPayout> HostPayouts { get; set; } = new List<HostPayout>();
        public ICollection<Dispute> Disputes { get; set; } = new List<Dispute>();
        public Conversation? Conversation { get; set; }
        public ICollection<UserReview> UserReviews { get; set; } = new List<UserReview>();
        public ListingReview? ListingReview { get; set; }
        public ICollection<AvailabilityBlock> AvailabilityBlocks { get; set; } = new List<AvailabilityBlock>();
    }
}
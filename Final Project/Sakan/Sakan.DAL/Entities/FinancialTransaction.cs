using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Sakan.DAL.Entities
{
    public class FinancialTransaction
    {
        public int Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }                // > 0
        public string Currency { get; set; } = "EGP";
        public Enums.Finance.TransactionStatus Status { get; set; }
        public string? ExternalReference { get; set; }
        public string IdempotencyKey { get; set; } = string.Empty; // unique
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        //navigation

        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public int? PaymentAuthorizationId { get; set; }   // guest-side types
        public PaymentAuthorization? PaymentAuthorization { get; set; }

        public int? HostPayoutId { get; set; }             // Payout / PlatformFee / PenaltyDeduction
        public HostPayout? HostPayout { get; set; }

        public int? DisputeResolutionId { get; set; }      // traceability
        public DisputeResolution? DisputeResolution { get; set; }

        public string? InitiatedById { get; set; }         // null = system
        public ApplicationUser? InitiatedBy { get; set; }
    }
}

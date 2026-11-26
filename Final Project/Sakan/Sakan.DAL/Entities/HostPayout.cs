using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Entities
{
    public class HostPayout
    {
        public int Id { get; set; }
        public PayoutType PayoutType { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal PenaltyDeductionAmount { get; set; }
        public decimal NetAmount { get; set; }             // = Gross - Commission - PenaltyDeduction
        public string Currency { get; set; } = "EGP";
        public PayoutStatus Status { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public DateTime? PaidAt { get; set; }

        //navigation

        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public string HostId { get; set; } = string.Empty;
        public ApplicationUser Host { get; set; } = null!;

        public ICollection<FinancialTransaction> Transactions { get; set; } = new List<FinancialTransaction>();
        public ICollection<HostPenaltyDeduction> PenaltyDeductions { get; set; } = new List<HostPenaltyDeduction>();
    }
}

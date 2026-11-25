using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Entities
{
    internal class HostPenalty
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingAmount { get; set; }
        public HostPenaltyStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        //navigation
        public string HostId { get; set; } = string.Empty;
        public ApplicationUser Host { get; set; } = null!;

        public int CancelledBookingId { get; set; }        // FK, unique
        public Booking CancelledBooking { get; set; } = null!;

        public ICollection<HostPenaltyDeduction> Deductions { get; set; } = new List<HostPenaltyDeduction>();
    }
}

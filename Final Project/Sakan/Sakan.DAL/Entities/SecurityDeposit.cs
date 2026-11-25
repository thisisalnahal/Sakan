using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Entities
{
    internal class SecurityDeposit
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal CapturedAmount { get; set; }        // cache, updated with the ledger
        public SecurityDepositStatus Status { get; set; }
        public DateTime AuthorizedAt { get; set; }
        public DateTime? ReleasedAt { get; set; }

        //navifation
        public int BookingId { get; set; }                 // FK, unique
        public Booking Booking { get; set; } = null!;
    }
}

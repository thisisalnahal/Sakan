using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Entities
{
    internal class PaymentAuthorization
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }                // = Booking.TotalAmount
        public string Currency { get; set; } = "EGP";      // char(3)
        public PaymentAuthorizationStatus Status { get; set; }
        public string IdempotencyKey { get; set; } = string.Empty; // unique (M4 handbook)
        public DateTime AuthorizedAt { get; set; }
        public DateTime? AuthorizationExpiresAt { get; set; }

        // navigation
        public int BookingId { get; set; }                 // FK, unique
        public Booking Booking { get; set; } = null!;
        public int PaymentMethodId { get; set; }           // FK
        public PaymentMethod PaymentMethod { get; set; } = null!;
        public List<FinancialTransaction> Transactions { get; set; } = new List<FinancialTransaction>();
    }
}

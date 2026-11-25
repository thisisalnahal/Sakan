using Sakan.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IPaymentAuthorizationRepository
    {
        Task<PaymentAuthorization?> GetByBookingIdAsync(int bookingId);
        Task AddAsync(PaymentAuthorization authorization);
    }
}

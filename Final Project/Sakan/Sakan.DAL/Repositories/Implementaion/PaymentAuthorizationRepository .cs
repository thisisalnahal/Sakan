using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class PaymentAuthorizationRepository : IPaymentAuthorizationRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentAuthorizationRepository(ApplicationDbContext context) => _context = context;

        public Task<PaymentAuthorization?> GetByBookingIdAsync(int bookingId) =>
            _context.PaymentAuthorizations.FirstOrDefaultAsync(a => a.BookingId == bookingId);

        public async Task AddAsync(PaymentAuthorization authorization) =>
            await _context.PaymentAuthorizations.AddAsync(authorization);
    }
}

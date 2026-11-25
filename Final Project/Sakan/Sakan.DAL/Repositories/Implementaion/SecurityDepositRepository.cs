using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Enums.Finance;
using Sakan.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class SecurityDepositRepository : ISecurityDepositRepository
    {
        private readonly ApplicationDbContext _context;
        public SecurityDepositRepository(ApplicationDbContext context) => _context = context;

        public Task<SecurityDeposit?> GetByBookingIdAsync(int bookingId) =>
            _context.SecurityDeposits.FirstOrDefaultAsync(d => d.BookingId == bookingId);

        public async Task AddAsync(SecurityDeposit deposit) =>
            await _context.SecurityDeposits.AddAsync(deposit);

        public Task<List<SecurityDeposit>> GetReleasableAsync(DateTime utcNow) =>
            _context.SecurityDeposits
                .Include(d => d.Booking)
                .Where(d => d.Status == SecurityDepositStatus.Authorized
                            && d.Booking.DamageWindowEndsAt <= utcNow)
                .ToListAsync();
    }
}

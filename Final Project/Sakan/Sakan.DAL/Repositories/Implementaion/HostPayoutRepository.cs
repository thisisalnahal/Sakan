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
    internal class HostPayoutRepository : IHostPayoutRepository
    {
        private readonly ApplicationDbContext _context;
        public HostPayoutRepository(ApplicationDbContext context) => _context = context;

        public Task<HostPayout?> GetByBookingAndTypeAsync(int bookingId, PayoutType payoutType) =>
            _context.HostPayouts.FirstOrDefaultAsync(p => p.BookingId == bookingId && p.PayoutType == payoutType);

        public Task<List<HostPayout>> GetByHostIdAsync(string hostId) =>
            _context.HostPayouts
                .AsNoTracking()
                .Where(p => p.HostId == hostId)
                .OrderByDescending(p => p.ScheduledAt)
                .ToListAsync();

        public async Task AddAsync(HostPayout payout) =>
            await _context.HostPayouts.AddAsync(payout);

        public Task<List<HostPayout>> GetDueRentalPayoutsAsync(DateTime utcNow) =>
            _context.HostPayouts
                .Where(p => p.PayoutType == PayoutType.RentalRelease
                            && p.Status == PayoutStatus.Pending
                            && p.ScheduledAt <= utcNow)
                .ToListAsync();
    }
}

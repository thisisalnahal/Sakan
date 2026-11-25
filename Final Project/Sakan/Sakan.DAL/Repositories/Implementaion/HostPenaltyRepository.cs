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
    internal class HostPenaltyRepository : IHostPenaltyRepository
    {
        private readonly ApplicationDbContext _context;
        public HostPenaltyRepository(ApplicationDbContext context) => _context = context;

        public Task<HostPenalty?> GetByIdAsync(int id) =>
            _context.HostPenalties.FirstOrDefaultAsync(p => p.Id == id);

        public Task<HostPenalty?> GetByCancelledBookingIdAsync(int cancelledBookingId) =>
            _context.HostPenalties.FirstOrDefaultAsync(p => p.CancelledBookingId == cancelledBookingId);

        public Task<List<HostPenalty>> GetOpenByHostIdAsync(string hostId) =>
            _context.HostPenalties
                .Where(p => p.HostId == hostId && p.Status == HostPenaltyStatus.Open)
                .OrderBy(p => p.CreatedAt)
                .ToListAsync();

        public async Task AddAsync(HostPenalty penalty) =>
            await _context.HostPenalties.AddAsync(penalty);
    }
}

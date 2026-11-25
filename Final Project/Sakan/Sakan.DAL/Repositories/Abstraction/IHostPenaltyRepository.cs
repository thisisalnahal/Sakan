using Sakan.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IHostPenaltyRepository
    {
        Task<HostPenalty?> GetByIdAsync(int id);
        Task<HostPenalty?> GetByCancelledBookingIdAsync(int cancelledBookingId);

        // Open penalties of a host, oldest first (used when deducting from a payout)
        Task<List<HostPenalty>> GetOpenByHostIdAsync(string hostId);

        Task AddAsync(HostPenalty penalty);
    }
}

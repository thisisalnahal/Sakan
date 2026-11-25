using Sakan.DAL.Entities;
using Sakan.DAL.Enums.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IHostPayoutRepository
    {
        Task<HostPayout?> GetByBookingAndTypeAsync(int bookingId, PayoutType payoutType);
        Task<List<HostPayout>> GetByHostIdAsync(string hostId);
        Task AddAsync(HostPayout payout);

        // For the maintenance job: RentalRelease payouts that are Pending and due
        Task<List<HostPayout>> GetDueRentalPayoutsAsync(DateTime utcNow);
    }
}

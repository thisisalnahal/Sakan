using Sakan.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface ISecurityDepositRepository
    {
        Task<SecurityDeposit?> GetByBookingIdAsync(int bookingId);
        Task AddAsync(SecurityDeposit deposit);

        // For the maintenance job: Authorized deposits whose damage window has ended
        Task<List<SecurityDeposit>> GetReleasableAsync(DateTime utcNow);
    }
}

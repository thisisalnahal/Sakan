using Sakan.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IFinancialTransactionRepository
    {
        Task<FinancialTransaction?> GetByIdempotencyKeyAsync(string idempotencyKey);
        Task<List<FinancialTransaction>> GetByBookingIdAsync(int bookingId);
        Task AddAsync(FinancialTransaction transaction);
    }
}

using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public FinancialTransactionRepository(ApplicationDbContext context) => _context = context;

        public Task<FinancialTransaction?> GetByIdempotencyKeyAsync(string idempotencyKey) =>
            _context.FinancialTransactions.FirstOrDefaultAsync(t => t.IdempotencyKey == idempotencyKey);

        public Task<List<FinancialTransaction>> GetByBookingIdAsync(int bookingId) =>
            _context.FinancialTransactions
                .AsNoTracking()
                .Where(t => t.BookingId == bookingId)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync();

        public async Task AddAsync(FinancialTransaction transaction) =>
            await _context.FinancialTransactions.AddAsync(transaction);
    }
}

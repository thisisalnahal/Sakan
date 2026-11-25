using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class HostPenaltyDeductionRepository : IHostPenaltyDeductionRepository
    {
        private readonly ApplicationDbContext _context;
        public HostPenaltyDeductionRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(HostPenaltyDeduction deduction) =>
            await _context.HostPenaltyDeductions.AddAsync(deduction);
    }
}

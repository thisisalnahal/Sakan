using Sakan.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IHostPenaltyDeductionRepository
    {
        Task AddAsync(HostPenaltyDeduction deduction);
    }
}

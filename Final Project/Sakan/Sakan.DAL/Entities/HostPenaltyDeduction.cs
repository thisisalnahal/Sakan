using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Entities
{
    public class HostPenaltyDeduction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        //navigation
        public int HostPenaltyId { get; set; }
        public HostPenalty HostPenalty { get; set; } = null!;
        public int HostPayoutId { get; set; }
        public HostPayout HostPayout { get; set; } = null!;
    }
}

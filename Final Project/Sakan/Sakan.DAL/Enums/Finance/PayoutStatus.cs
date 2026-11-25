using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Enums.Finance
{
    public enum PayoutStatus 
    { 
        Pending, 
        OnHold, 
        Paid, 
        Cancelled, 
        Failed 
    }
}

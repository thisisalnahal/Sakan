using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Enums.Finance
{
    public enum TransactionType
    {
        // Guest side : PaymentAuthorizationId
        Authorization, AuthorizationRelease, Capture, Refund, SecurityCapture, SecurityRelease,

        // Host / platform side : HostPayoutId
        Payout, PlatformFee, PenaltyDeduction
    }
}

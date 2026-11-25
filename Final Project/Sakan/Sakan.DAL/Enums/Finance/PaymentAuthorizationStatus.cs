using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Enums.Finance
{
    public enum PaymentAuthorizationStatus
    {
        Authorized, 
        Captured, 
        Released, 
        PartiallyCaptured 
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface ICancellationPolicyRepository
    {
        (bool, string?) Create(CancellationPolicy policy);
        (bool, string?) Edit(CancellationPolicy newPolicy, int oldPolicyId);
        (bool, string?, List<CancellationPolicy>?) GetAll();
        (bool, string?, CancellationPolicy?) GetById(int policyId);
        (bool, string?) Delete(int policyId);
    }
} 


using System;
using System.Collections.Generic;
using System.Text;

using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface ICancellationPolicyRuleRepository
    {
        (bool, string?) Create(CancellationPolicyRule rule);
        (bool, string?) Edit(CancellationPolicyRule newRule, int oldRuleId);
        (bool, string?, List<CancellationPolicyRule>?) GetAll();
        (bool, string?, List<CancellationPolicyRule>?) GetByPolicyId(int policyId);
        (bool, string?, CancellationPolicyRule?) GetById(int ruleId);
        (bool, string?) Delete(int ruleId);
    }
} 


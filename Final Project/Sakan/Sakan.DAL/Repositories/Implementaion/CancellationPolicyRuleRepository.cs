using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    public class CancellationPolicyRuleRepository : ICancellationPolicyRuleRepository
    {
        private readonly ApplicationDbContext Db;

        internal CancellationPolicyRuleRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(CancellationPolicyRule rule)
        {
            try
            {
                bool policyExists = Db.CancellationPolicies.Any(p => p.Id == rule.PolicyId);
                if (!policyExists)
                    return (true, "CancellationPolicy not found");

                if (rule.MinHoursBeforeCheckIn < 0)
                    return (true, "MinHoursBeforeCheckIn must be greater than or equal to 0");

                if (rule.RefundPercentage < 0 || rule.RefundPercentage > 100)
                    return (true, "RefundPercentage must be between 0 and 100");

                bool isInUse = Db.Listings.Any(l => l.CancellationPolicyId == rule.PolicyId) ||
                               Db.Bookings.Any(b => b.CancellationPolicyId == rule.PolicyId);

                if (isInUse)
                    return (true, "Cannot add rules to a CancellationPolicy that is already in use");

                Db.CancellationPolicyRules.Add(rule);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(CancellationPolicyRule newRule, int oldRuleId)
        {
            try
            {
                var oldRule = Db.CancellationPolicyRules.FirstOrDefault(r => r.Id == oldRuleId);
                if (oldRule == null)
                    return (true, "CancellationPolicyRule not found");

                if (newRule.MinHoursBeforeCheckIn < 0)
                    return (true, "MinHoursBeforeCheckIn must be greater than or equal to 0");

                if (newRule.RefundPercentage < 0 || newRule.RefundPercentage > 100)
                    return (true, "RefundPercentage must be between 0 and 100");

                bool isInUse = Db.Listings.Any(l => l.CancellationPolicyId == oldRule.PolicyId) ||
                               Db.Bookings.Any(b => b.CancellationPolicyId == oldRule.PolicyId);

                if (isInUse)
                    return (true, "Cannot edit rules of a CancellationPolicy that is already in use");

                oldRule.MinHoursBeforeCheckIn = newRule.MinHoursBeforeCheckIn;
                oldRule.RefundPercentage = newRule.RefundPercentage;

                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<CancellationPolicyRule>?) GetAll()
        {
            try
            {
                var rules = Db.CancellationPolicyRules
                    .Include(r => r.CancellationPolicy)
                    .ToList();

                return (false, null, rules);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, List<CancellationPolicyRule>?) GetByPolicyId(int policyId)
        {
            try
            {
                var rules = Db.CancellationPolicyRules
                    .Where(r => r.PolicyId == policyId)
                    .OrderByDescending(r => r.MinHoursBeforeCheckIn)
                    .ToList();

                return (false, null, rules);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, CancellationPolicyRule?) GetById(int ruleId)
        {
            try
            {
                var rule = Db.CancellationPolicyRules
                    .Include(r => r.CancellationPolicy)
                    .FirstOrDefault(r => r.Id == ruleId);

                if (rule == null)
                    return (true, "CancellationPolicyRule not found", null);

                return (false, null, rule);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int ruleId)
        {
            try
            {
                var rule = Db.CancellationPolicyRules.FirstOrDefault(r => r.Id == ruleId);
                if (rule == null)
                    return (true, "CancellationPolicyRule not found");

                bool isInUse = Db.Listings.Any(l => l.CancellationPolicyId == rule.PolicyId) ||
                               Db.Bookings.Any(b => b.CancellationPolicyId == rule.PolicyId);

                if (isInUse)
                    return (true, "Cannot delete rules of a CancellationPolicy that is already in use");

                Db.CancellationPolicyRules.Remove(rule);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }
    }
}

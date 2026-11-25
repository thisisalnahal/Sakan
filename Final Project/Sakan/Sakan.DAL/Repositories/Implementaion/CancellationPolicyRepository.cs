using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;
using Sakan.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakan.DAL.Repositories.Implementaion
{
    public class CancellationPolicyRepository : ICancellationPolicyRepository
    {
        private readonly ApplicationDbContext Db;

        public CancellationPolicyRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(CancellationPolicy policy)
        {
            try
            {
                Db.CancellationPolicies.Add(policy);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(CancellationPolicy newPolicy, int oldPolicyId)
        {
            try
            {
                var oldPolicy = Db.CancellationPolicies
                    .Include(p => p.CancellationPolicyRules)
                    .FirstOrDefault(p => p.Id == oldPolicyId);

                if (oldPolicy == null)
                    return (true, "CancellationPolicy not found");

                bool isInUse = Db.Listings.Any(l => l.CancellationPolicyId == oldPolicyId) ||
                               Db.Bookings.Any(b => b.CancellationPolicyId == oldPolicyId);

                if (isInUse)
                    return (true, "Cannot edit an immutable CancellationPolicy that is already in use; create a new policy instead");

                oldPolicy.Name = newPolicy.Name;
                oldPolicy.Description = newPolicy.Description;
                oldPolicy.IsActive = newPolicy.IsActive;

                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<CancellationPolicy>?) GetAll()
        {
            try
            {
                var policies = Db.CancellationPolicies
                    .Include(p => p.CancellationPolicyRules)
                    .ToList();

                return (false, null, policies);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, CancellationPolicy?) GetById(int policyId)
        {
            try
            {
                var policy = Db.CancellationPolicies
                    .Include(p => p.CancellationPolicyRules)
                    .FirstOrDefault(p => p.Id == policyId);

                if (policy == null)
                    return (true, "CancellationPolicy not found", null);

                return (false, null, policy);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int policyId)
        {
            try
            {
                var policy = Db.CancellationPolicies.FirstOrDefault(p => p.Id == policyId);
                if (policy == null)
                    return (true, "CancellationPolicy not found");

                bool isInUse = Db.Listings.Any(l => l.CancellationPolicyId == policyId) ||
                               Db.Bookings.Any(b => b.CancellationPolicyId == policyId);

                if (isInUse)
                    return (true, "Cannot delete a CancellationPolicy that is already referenced");

                Db.CancellationPolicies.Remove(policy);
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

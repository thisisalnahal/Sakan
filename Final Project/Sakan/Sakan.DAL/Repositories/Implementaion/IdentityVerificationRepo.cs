using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Enums.IdentityVerification;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class IdentityVerificationRepo : IIdentityVerificationRepo
    {
        private readonly ApplicationDbContext _db;

        public IdentityVerificationRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public (bool Success, string Message) Add(IdentityVerification verification)
        {
            try
            {
                if (_db.IdentityVerifications.Any(v => v.UserId == verification.UserId && v.Status == Status.Pending))
                    return (false, "You already have a pending verification request.");

                _db.IdentityVerifications.Add(verification);
                _db.SaveChanges();
                _db.ChangeTracker.Clear();
                return (true, "Verification request submitted.");
            }
            catch (Exception ex)
            {
                _db.ChangeTracker.Clear();
                return (false, "Database error: " + ex.Message);
            }
        }

        public (bool Success, string Message, IdentityVerification? Data) GetById(int id)
        {
            try
            {
                var item = _db.IdentityVerifications.AsNoTracking()
                    .Include(v => v.User)
                    .FirstOrDefault(v => v.Id == id);

                if (item == null)
                    return (false, "Verification request not found.", null);

                return (true, "", item);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, null);
            }
        }

        public (bool Success, string Message, IdentityVerification? Data) GetLatestByUser(string userId)
        {
            try
            {
                var item = _db.IdentityVerifications.AsNoTracking()
                    .Where(v => v.UserId == userId)
                    .OrderByDescending(v => v.SubmittedAt).ThenByDescending(v => v.Id)
                    .FirstOrDefault();

                if (item == null)
                    return (false, "No verification request found.", null);

                return (true, "", item);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, null);
            }
        }

        public (bool Success, string Message, List<IdentityVerification> Data, int TotalCount) GetPaged(
            Status? status, int page, int pageSize)
        {
            try
            {
                var query = _db.IdentityVerifications.AsNoTracking().Include(v => v.User).AsQueryable();

                if (status.HasValue)
                    query = query.Where(v => v.Status == status.Value);

                if (page < 1) page = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 100) pageSize = 100;

                var total = query.Count();
                var list = query
                    .OrderBy(v => v.SubmittedAt).ThenBy(v => v.Id)
                    .Skip((page - 1) * pageSize).Take(pageSize)
                    .ToList();

                return (true, "", list, total);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, new List<IdentityVerification>(), 0);
            }
        }

        public (bool Success, string Message) Update(IdentityVerification verification)
        {
            try
            {
                var existing = _db.IdentityVerifications.Find(verification.Id);
                if (existing == null)
                    return (false, "Verification request not found.");

                _db.Entry(existing).CurrentValues.SetValues(verification);
                _db.SaveChanges();
                _db.ChangeTracker.Clear();
                return (true, "Verification request updated.");
            }
            catch (Exception ex)
            {
                _db.ChangeTracker.Clear();
                return (false, "Database error: " + ex.Message);
            }
        }
    }
}

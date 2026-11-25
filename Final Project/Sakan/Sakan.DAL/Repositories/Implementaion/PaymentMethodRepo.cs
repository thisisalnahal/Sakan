using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class PaymentMethodRepo : IPaymentMethodRepo
    {
        private readonly ApplicationDbContext _db;

        public PaymentMethodRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public (bool Success, string Message) Add(PaymentMethod method)
        {
            try
            {
                // The transaction makes "clear the old default" and "add the new one" succeed or fail together.
                using var tx = _db.Database.BeginTransaction();

                if (method.IsDefault)
                    ClearDefault(method.UserId);

                _db.PaymentMethods.Add(method);
                _db.SaveChanges();
                tx.Commit();

                _db.ChangeTracker.Clear();
                return (true, "Payment method added.");
            }
            catch (Exception ex)
            {
                _db.ChangeTracker.Clear();
                return (false, "Database error: " + ex.Message);
            }
        }

        public (bool Success, string Message, PaymentMethod? Data) GetById(int id)
        {
            try
            {
                var item = _db.PaymentMethods.AsNoTracking().FirstOrDefault(p => p.Id == id);
                if (item == null)
                    return (false, "Payment method not found.", null);

                return (true, "", item);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, null);
            }
        }

        public (bool Success, string Message, List<PaymentMethod> Data) GetByUser(string userId, bool activeOnly)
        {
            try
            {
                var query = _db.PaymentMethods.AsNoTracking().Where(p => p.UserId == userId);

                if (activeOnly)
                    query = query.Where(p => p.IsActive);

                var list = query.OrderByDescending(p => p.IsDefault).ThenBy(p => p.Id).ToList();
                return (true, "", list);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, new List<PaymentMethod>());
            }
        }

        public (bool Success, string Message, PaymentMethod? Data) GetDefault(string userId)
        {
            try
            {
                var item = _db.PaymentMethods.AsNoTracking()
                    .FirstOrDefault(p => p.UserId == userId && p.IsDefault && p.IsActive);

                if (item == null)
                    return (false, "No default payment method found.", null);

                return (true, "", item);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, null);
            }
        }

        public (bool Success, string Message) SetDefault(string userId, int methodId)
        {
            try
            {
                using var tx = _db.Database.BeginTransaction();

                var method = _db.PaymentMethods
                    .FirstOrDefault(p => p.Id == methodId && p.UserId == userId && p.IsActive);

                if (method == null)
                    return (false, "Payment method not found or not active.");

                ClearDefault(userId);

                method.IsDefault = true;
                _db.SaveChanges();
                tx.Commit();

                _db.ChangeTracker.Clear();
                return (true, "Default payment method updated.");
            }
            catch (Exception ex)
            {
                _db.ChangeTracker.Clear();
                return (false, "Database error: " + ex.Message);
            }
        }

        public (bool Success, string Message) Update(PaymentMethod method)
        {
            try
            {
                var existing = _db.PaymentMethods.Find(method.Id);
                if (existing == null)
                    return (false, "Payment method not found.");

                _db.Entry(existing).CurrentValues.SetValues(method);
                _db.SaveChanges();
                _db.ChangeTracker.Clear();
                return (true, "Payment method updated.");
            }
            catch (Exception ex)
            {
                _db.ChangeTracker.Clear();
                return (false, "Database error: " + ex.Message);
            }
        }

        // The unique index allows only one default per user, so the old one must be cleared first.
        private void ClearDefault(string userId)
        {
            var current = _db.PaymentMethods.Where(p => p.UserId == userId && p.IsDefault).ToList();
            foreach (var item in current)
                item.IsDefault = false;

            _db.SaveChanges();
        }
    }
}

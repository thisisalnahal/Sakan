using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class ApplicationUserRepo : IApplicationUserRepo
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public (bool Success, string Message, ApplicationUser? Data) GetById(string id)
        {
            try
            {
                var user = _db.Users.AsNoTracking().FirstOrDefault(u => u.ID == id);
                if (user == null)
                    return (false, "User not found.", null);

                return (true, "", user);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, null);
            }
        }

        public (bool Success, string Message, ApplicationUser? Data) GetByEmail(string email)
        {
            try
            {
                var user = _db.Users.AsNoTracking().FirstOrDefault(u => u.Email == email);
                if (user == null)
                    return (false, "User not found.", null);

                return (true, "", user);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, null);
            }
        }

        public (bool Success, string Message, List<ApplicationUser> Data, int TotalCount) Search(
            string? term, bool? isActive, int page, int pageSize)
        {
            try
            {
                var query = _db.Users.AsNoTracking();

                if (isActive.HasValue)
                    query = query.Where(u => u.IsActive == isActive.Value);

                if (!string.IsNullOrWhiteSpace(term))
                {
                    var t = term.Trim();
                    query = query.Where(u => u.FullName.Contains(t)
                                          || u.DisplayName.Contains(t)
                                          || u.Email.Contains(t));
                }

                if (page < 1) page = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 100) pageSize = 100;

                var total = query.Count();
                var list = query
                    .OrderBy(u => u.FullName).ThenBy(u => u.ID)
                    .Skip((page - 1) * pageSize).Take(pageSize)
                    .ToList();

                return (true, "", list, total);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, new List<ApplicationUser>(), 0);
            }
        }

        public (bool Success, string Message) Add(ApplicationUser user)
        {
            try
            {
                if (_db.Users.Any(u => u.Email == user.Email))
                    return (false, "Email is already registered.");

                _db.Users.Add(user);
                _db.SaveChanges();
                _db.ChangeTracker.Clear();
                return (true, "User created.");
            }
            catch (Exception ex)
            {
                _db.ChangeTracker.Clear();
                return (false, "Database error: " + ex.Message);
            }
        }

        public (bool Success, string Message) Update(ApplicationUser user)
        {
            try
            {
                var existing = _db.Users.Find(user.ID);
                if (existing == null)
                    return (false, "User not found.");

                // Copies the normal columns only (navigation properties are ignored).
                _db.Entry(existing).CurrentValues.SetValues(user);
                _db.SaveChanges();
                _db.ChangeTracker.Clear();
                return (true, "User updated.");
            }
            catch (Exception ex)
            {
                _db.ChangeTracker.Clear();
                return (false, "Database error: " + ex.Message);
            }
        }
    }
}

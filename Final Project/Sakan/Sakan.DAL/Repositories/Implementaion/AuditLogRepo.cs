using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class AuditLogRepo : IAuditLogRepo
    {
        private readonly ApplicationDbContext _db;

        public AuditLogRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public (bool Success, string Message) Add(AuditLog log)
        {
            try
            {
                _db.AuditLogs.Add(log);
                _db.SaveChanges();
                _db.ChangeTracker.Clear();
                return (true, "Logged.");
            }
            catch (Exception ex)
            {
                _db.ChangeTracker.Clear();
                return (false, "Database error: " + ex.Message);
            }
        }

        public (bool Success, string Message, List<AuditLog> Data, int TotalCount) GetPaged(
            string? actorId, string? entityType, DateTime? fromUtc, DateTime? toUtc,
            int page, int pageSize)
        {
            try
            {
                var query = _db.AuditLogs.AsNoTracking().Include(a => a.Actor).AsQueryable();

                if (!string.IsNullOrWhiteSpace(actorId))
                    query = query.Where(a => a.ActorId == actorId);
                if (!string.IsNullOrWhiteSpace(entityType))
                    query = query.Where(a => a.EntityType == entityType);
                if (fromUtc.HasValue)
                    query = query.Where(a => a.At >= fromUtc.Value);
                if (toUtc.HasValue)
                    query = query.Where(a => a.At < toUtc.Value);

                if (page < 1) page = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 100) pageSize = 100;

                var total = query.Count();
                var list = query
                    .OrderByDescending(a => a.At).ThenByDescending(a => a.Id)
                    .Skip((page - 1) * pageSize).Take(pageSize)
                    .ToList();

                return (true, "", list, total);
            }
            catch (Exception ex)
            {
                return (false, "Database error: " + ex.Message, new List<AuditLog>(), 0);
            }
        }
    }
}

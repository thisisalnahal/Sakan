using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IAuditLogRepo
    {
        (bool Success, string Message) Add(AuditLog log);

        (bool Success, string Message, List<AuditLog> Data, int TotalCount) GetPaged(
            string? actorId, string? entityType, DateTime? fromUtc, DateTime? toUtc,
            int page, int pageSize);
    }
}

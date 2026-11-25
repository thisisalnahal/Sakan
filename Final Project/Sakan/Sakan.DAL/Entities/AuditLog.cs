namespace Sakan.DAL.Entities
{
    // Append-only: no update and no delete anywhere in the code.
    internal class AuditLog
    {
        public int Id { get; set; }
        public string ActorId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string? Details { get; set; }
        public DateTime At { get; set; }

        //nav. prop.
        public ApplicationUser Actor { get; set; } = null!;
    }
}

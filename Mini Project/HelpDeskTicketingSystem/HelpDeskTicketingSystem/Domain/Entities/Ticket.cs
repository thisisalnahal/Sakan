using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Domain.Entities
{
    internal class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority? Priority { get; set; }
        public Department? Department { get; set; }
        public int ClientId { get; set; }
        public int? AssignedUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public bool IsOverdue()
        {
            if (DueDate == null) return false;
            if (Status == TicketStatus.Resolved || Status == TicketStatus.Closed) return false;
            return DateTime.Now > DueDate.Value;
        }
    }
}
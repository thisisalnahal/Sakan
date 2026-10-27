using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Domain.Entities
{
    internal class Agent : User
    {
        public override Role Role => Role.Agent;

        public Department Department { get; set; }

        public DateTime? LastAssignedAt { get; set; }
    }
}
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Domain.Entities
{
    internal class Client : User
    {
        public override Role Role => Role.Client;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
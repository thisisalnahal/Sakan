using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Domain.Entities
{
    internal class Client : User
    {
        public override Role Role => Role.Client;
    }
}
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Domain.Entities
{
    internal class Dispatcher : User
    {
        public override Role Role => Role.Dispatcher;
    }
}
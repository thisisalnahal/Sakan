using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Domain.Entities
{
    internal class Admin : User
    {
        public override Role Role => Role.Admin;
    }
}
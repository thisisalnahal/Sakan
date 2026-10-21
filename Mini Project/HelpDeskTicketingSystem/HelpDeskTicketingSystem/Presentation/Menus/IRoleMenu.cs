using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Presentation
{
    internal interface IRoleMenu
    {
        Role Role { get; }

        void Show(User user);
    }
}
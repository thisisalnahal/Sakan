using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Presentation.Helpers;
using HelpDeskTicketingSystem.Presentation.UI;

namespace HelpDeskTicketingSystem.Presentation.Menus
{
    internal class AdminMenu : IRoleMenu
    {
        public Role Role => throw new NotImplementedException();

        public void Show(User user)
        {
            throw new NotImplementedException();
        }
    }
}
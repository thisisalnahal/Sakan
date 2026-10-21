using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Presentation
{
    internal class AdminMenu : IRoleMenu
    {
        private readonly IConsoleUI _ui;

        public AdminMenu(IConsoleUI ui)
        {
            _ui = ui;
        }

        public Role Role => Role.Admin;

        public void Show(User user)
        {
            _ui.PrintWarning("Admin menu not implemented yet.");
            _ui.Pause();
        }
    }
}
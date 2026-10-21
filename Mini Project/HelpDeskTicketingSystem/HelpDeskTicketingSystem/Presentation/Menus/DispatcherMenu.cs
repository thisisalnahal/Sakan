using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Presentation
{
    internal class DispatcherMenu : IRoleMenu
    {
        private readonly IConsoleUI _ui;

        public DispatcherMenu(IConsoleUI ui)
        {
            _ui = ui;
        }

        public Role Role => Role.Dispatcher;

        public void Show(User user)
        {
            _ui.PrintWarning("Dispatcher menu not implemented yet.");
            _ui.Pause();
        }
    }
}
using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Presentation
{
    internal class ClientMenu : IRoleMenu
    {
        private readonly IConsoleUI _ui;

        public ClientMenu(IConsoleUI ui)
        {
            _ui = ui;
        }

        public Role Role => Role.Client;

        public void Show(User user)
        {
            _ui.PrintWarning("Client menu not implemented yet.");
            _ui.Pause();
        }
    }
}
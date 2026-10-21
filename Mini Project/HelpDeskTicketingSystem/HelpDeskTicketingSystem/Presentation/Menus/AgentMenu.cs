using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Presentation
{
    internal class AgentMenu : IRoleMenu
    {
        private readonly IConsoleUI _ui;

        public AgentMenu(IConsoleUI ui)
        {
            _ui = ui;
        }

        public Role Role => Role.Agent;

        public void Show(User user)
        {
            _ui.PrintWarning("Agent menu not implemented yet.");
            _ui.Pause();
        }
    }
}
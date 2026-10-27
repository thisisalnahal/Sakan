using HelpDeskTicketingSystem.Presentation.UI;
using HelpDeskTicketingSystem.Domain.Entities;

namespace HelpDeskTicketingSystem.Presentation.Menus
{
    internal class MenuRouter
    {
        private readonly IEnumerable<IRoleMenu> _menus;
        private readonly IConsoleUI _ui;

        public MenuRouter(IEnumerable<IRoleMenu> menus, IConsoleUI ui)
        {
            _menus = menus;
            _ui = ui;
        }

        public void Show(User user)
        {
            IRoleMenu? menu = _menus.FirstOrDefault(m => m.Role == user.Role);

            if (menu == null)
            {
                _ui.PrintError("No menu is available for this account type.");
                _ui.Pause();
                return;
            }

            menu.Show(user);
        }
    }
}
using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Interfaces;

namespace HelpDeskTicketingSystem.Presentation
{
    internal class WelcomeMenu
    {
        private readonly IConsoleUI _ui;
        private readonly IAuthService _authService;
        private readonly IAutoCloseService _autoCloseService;
        private readonly MenuRouter _router;

        public WelcomeMenu(IConsoleUI ui, IAuthService authService, IAutoCloseService autoCloseService, MenuRouter router)
        {
            _ui = ui;
            _authService = authService;
            _autoCloseService = autoCloseService;
            _router = router;
        }

        public void Run()
        {
            while (true)
            {
                _autoCloseService.CloseExpiredResolvedTickets();

                _ui.ClearScreen();
                int choice = _ui.PrintMenu("HELP DESK TICKETING SYSTEM",
                [
                    "Login",
                    "Register",
                    "Exit"
                ]);

                if (choice == 1)
                {
                    Login();
                }
                else if (choice == 2)
                {
                    Register();
                }
                else
                {
                    _ui.PrintLine("Goodbye!");
                    return;
                }
            }
        }

        private void Login()
        {
            _ui.ClearScreen();
            _ui.PrintHeader("LOGIN");

            string email = _ui.ReadEmail("Email: ");
            string password = _ui.ReadPassword("Password: ");

            User? user = _authService.Login(email, password);

            if (user == null)
            {
                _ui.PrintError("Invalid email or password.");
                _ui.Pause();
                return;
            }

            if (user.MustChangePassword)
            {
                _ui.PrintWarning("You must change your password before continuing.");
                string newPassword = _ui.ReadPassword("New password: ");
                _authService.CompleteForcedPasswordChange(user.Id, newPassword);
                _ui.PrintSuccess("Password changed.");
                _ui.Pause();
            }

            _router.Show(user);
        }

        private void Register()
        {
            _ui.ClearScreen();
            _ui.PrintHeader("CLIENT REGISTRATION");

            string name = _ui.ReadName("Enter your name: ");
            string email = _ui.ReadEmail("Enter your email: ");
            string password = _ui.ReadPassword("Enter your password: ");

            if (_authService.TryRegisterClient(name, email, password, out string error))
                _ui.PrintSuccess("Account created. You can now log in.");
            else
                _ui.PrintError(error);

            _ui.Pause();
        }
    }
}
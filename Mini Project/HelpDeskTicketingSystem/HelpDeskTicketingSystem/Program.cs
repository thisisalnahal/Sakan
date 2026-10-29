using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Infrastructure.Security;
using HelpDeskTicketingSystem.Interfaces.Auth;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Interfaces.Tickets;
using HelpDeskTicketingSystem.Presentation.Helpers;
using HelpDeskTicketingSystem.Presentation.Menus;
using HelpDeskTicketingSystem.Presentation.UI;
using HelpDeskTicketingSystem.Services.Auth;
using HelpDeskTicketingSystem.Services.Roles;
using HelpDeskTicketingSystem.Services.Tickets;

namespace HelpDeskTicketingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataStore store = new DataStore();

            IPasswordHasher passwordHasher = new PasswordHasher();
            new DataSeeder(store, passwordHasher).Seed();

            IConsoleUI ui = new ConsoleUI();
            IUserDirectory userDirectory = new UserDirectory(store);
            TicketDisplayHelper ticketDisplay = new TicketDisplayHelper(ui, userDirectory);

            ITicketStatusService statusService = new TicketStatusService();
            ICommentPermissionService commentPermissionService = new CommentPermissionService();
            IDueDateCalculator dueDateCalculator = new DueDateCalculator();
            IAutoCloseService autoCloseService = new AutoCloseService(store, statusService);
            IAuthService authService = new AuthService(store, passwordHasher);

            IAdminService adminService = new AdminService(store, passwordHasher);
            IAgentService agentService = new AgentService(store, statusService, commentPermissionService);
            
            
            List<IRoleMenu> menus = new List<IRoleMenu>
            {
                new AdminMenu(ui, adminService, ticketDisplay),
                new AgentMenu(ui, agentService, commentPermissionService, ticketDisplay)
            };

            MenuRouter router = new MenuRouter(menus, ui);
            WelcomeMenu welcomeMenu = new WelcomeMenu(ui, authService, autoCloseService, router);

            welcomeMenu.Run();
        }
    }
}

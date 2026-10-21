using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Infrastructure.Security;
using HelpDeskTicketingSystem.Interfaces;
using HelpDeskTicketingSystem.Presentation;
using HelpDeskTicketingSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskTicketingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using AppDbContext db = new AppDbContext();
            db.Database.Migrate();

            IPasswordHasher passwordHasher = new PasswordHasher();
            new DbSeeder(db, passwordHasher).Seed();

            IConsoleUI ui = new ConsoleUI();
            TicketDisplayHelper ticketDisplay = new TicketDisplayHelper(ui);

            ITicketStatusService statusService = new TicketStatusService(db);
            ICommentPermissionService commentPermissionService = new CommentPermissionService();
            IDueDateCalculator dueDateCalculator = new DueDateCalculator();
            IAutoCloseService autoCloseService = new AutoCloseService(db, statusService);
            IAuthService authService = new AuthService(db, passwordHasher);

            List<IRoleMenu> menus = new List<IRoleMenu>
            {
                new ClientMenu(ui),
                new DispatcherMenu(ui),
                new AgentMenu(ui),
                new AdminMenu(ui)
            };

            MenuRouter router = new MenuRouter(menus, ui);
            WelcomeMenu welcomeMenu = new WelcomeMenu(ui, authService, autoCloseService, router);

            welcomeMenu.Run();
        }
    }
}
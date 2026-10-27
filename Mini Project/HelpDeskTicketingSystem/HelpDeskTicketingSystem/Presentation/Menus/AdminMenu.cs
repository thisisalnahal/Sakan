using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Presentation.Helpers;
using HelpDeskTicketingSystem.Presentation.UI;

namespace HelpDeskTicketingSystem.Presentation.Menus
{
    internal class AdminMenu : IRoleMenu
    {
        private readonly IConsoleUI _ui;
        private readonly IAdminService _adminService;
        private readonly TicketDisplayHelper _ticketDisplay;

        public AdminMenu(IConsoleUI ui, IAdminService adminService, TicketDisplayHelper ticketDisplay)
        {
            _ui = ui;
            _adminService = adminService;
            _ticketDisplay = ticketDisplay;
        }

        public Role Role => Role.Admin;

        public void Show(User user)
        {
            while (true)
            {
                _ui.ClearScreen();
                int choice = _ui.PrintMenu($"ADMIN MENU - {user.Name}",
                [
                    "Create agent account",
                    "Create dispatcher account",
                    "View / filter all users",
                    "View / filter all tickets",
                    "Logout"
                ]);

                switch (choice)
                {
                    case 1:
                        CreateAgent();
                        break;
                    case 2:
                        CreateDispatcher();
                        break;
                    case 3:
                        ViewUsers(user);
                        break;
                    case 4:
                        ViewTickets();
                        break;
                    case 5:
                        _ui.PrintInfo("Logging out...");
                        _ui.Pause();
                        return;
                }
            }
        }

        private void CreateAgent()
        {
            _ui.ClearScreen();
            _ui.PrintHeader("CREATE AGENT ACCOUNT");

            string name = _ui.ReadName("Agent name: ");
            string email = _ui.ReadEmail("Agent email: ");

            if (_adminService.GetUserByEmail(email) != null)
            {
                _ui.PrintError("An account with this email already exists.");
                _ui.Pause();
                return;
            }

            string password = _ui.ReadPassword("Temporary password: ");

            Department[] departments = Enum.GetValues<Department>()
                .Where(d => d != Department.GeneralQuestions)
                .ToArray();

            int choice = _ui.PrintMenu("Select the agent's department", departments.Select(d => d.ToString()).ToArray());
            Department department = departments[choice - 1];

            if (_adminService.TryCreateAgent(name, email, password, department, out string error))
                _ui.PrintSuccess($"Agent '{name}' created in the {department} department. The password must be changed at first login.");
            else
                _ui.PrintError(error);

            _ui.Pause();
        }

        private void CreateDispatcher()
        {
            _ui.ClearScreen();
            _ui.PrintHeader("CREATE DISPATCHER ACCOUNT");

            string name = _ui.ReadName("Dispatcher name: ");
            string email = _ui.ReadEmail("Dispatcher email: ");

            if (_adminService.GetUserByEmail(email) != null)
            {
                _ui.PrintError("An account with this email already exists.");
                _ui.Pause();
                return;
            }

            string password = _ui.ReadPassword("Temporary password: ");

            if (_adminService.TryCreateDispatcher(name, email, password, out string error))
                _ui.PrintSuccess($"Dispatcher '{name}' created. The password must be changed at first login.");
            else
                _ui.PrintError(error);

            _ui.Pause();
        }

        private void ViewUsers(User admin)
        {
            while (true)
            {
                _ui.ClearScreen();
                int choice = _ui.PrintMenu("VIEW / FILTER USERS",
                [
                    "All users",
                    "Clients only",
                    "Agents only",
                    "Dispatchers only",
                    "Admins only",
                    "Back"
                ]);

                if (choice == 6)
                    return;

                Role? role = choice switch
                {
                    2 => Role.Client,
                    3 => Role.Agent,
                    4 => Role.Dispatcher,
                    5 => Role.Admin,
                    _ => null
                };

                List<User> users = _adminService.GetUsers(role);

                if (users.Count == 0)
                {
                    _ui.PrintWarning("No users found for this filter.");
                    _ui.Pause();
                    continue;
                }

                _ui.ClearScreen();
                _ui.PrintHeader("USERS");

                List<string[]> rows = new List<string[]>();

                foreach (User u in users)
                {
                    rows.Add(new[]
                    {
                        u.Id.ToString(),
                        u.Name,
                        u.Email,
                        u.Role.ToString(),
                        u is Agent agent ? agent.Department.ToString() : "-"
                    });
                }

                _ui.PrintTable(new[] { "ID", "Name", "Email", "Role", "Department" }, rows);
                _ui.PrintDivider();

                int action = _ui.PrintMenu("Choose operation",
                [
                    "Delete a user",
                    "Back"
                ]);

                if (action == 1)
                    DeleteUser(admin);
            }
        }

        private void DeleteUser(User admin)
        {
            string email = _ui.ReadEmail("Email of the user to delete: ");
            User? target = _adminService.GetUserByEmail(email);

            if (target == null)
            {
                _ui.PrintError("User not found.");
                _ui.Pause();
                return;
            }

            if (!_ui.Confirm($"Delete user '{target.Name}'?"))
            {
                _ui.PrintInfo("Deletion cancelled.");
                _ui.Pause();
                return;
            }

            if (_adminService.TryDeleteUser(admin.Id, target.Id, out string error))
                _ui.PrintSuccess($"User '{target.Name}' deleted.");
            else
                _ui.PrintError(error);

            _ui.Pause();
        }

        private void ViewTickets()
        {
            while (true)
            {
                _ui.ClearScreen();
                int choice = _ui.PrintMenu("VIEW / FILTER TICKETS",
                [
                    "All tickets",
                    "Open",
                    "Assigned",
                    "In progress",
                    "Resolved",
                    "Closed",
                    "Back"
                ]);

                if (choice == 7)
                    return;

                TicketStatus? status = choice switch
                {
                    2 => TicketStatus.Open,
                    3 => TicketStatus.Assigned,
                    4 => TicketStatus.InProgress,
                    5 => TicketStatus.Resolved,
                    6 => TicketStatus.Closed,
                    _ => null
                };

                List<Ticket> tickets = _adminService.GetTickets(status);

                if (tickets.Count == 0)
                {
                    _ui.PrintWarning("No tickets found for this filter.");
                    _ui.Pause();
                    continue;
                }

                _ui.ClearScreen();
                _ui.PrintHeader("TICKETS");
                _ticketDisplay.PrintTicketTable(tickets);
                _ui.PrintDivider();

                int action = _ui.PrintMenu("Choose operation",
                [
                    "View ticket details",
                    "Delete a ticket",
                    "Back"
                ]);

                if (action == 1)
                    ViewTicketDetails();
                else if (action == 2)
                    DeleteTicket();
            }
        }

        private void ViewTicketDetails()
        {
            int ticketId = _ui.ReadInt("Ticket ID: ");
            Ticket? ticket = _adminService.GetTicket(ticketId);

            if (ticket == null)
            {
                _ui.PrintError("Ticket not found.");
                _ui.Pause();
                return;
            }

            _ui.ClearScreen();
            _ticketDisplay.PrintTicketDetails(ticket);
            _ui.Pause();
        }

        private void DeleteTicket()
        {
            int ticketId = _ui.ReadInt("ID of the ticket to delete: ");
            Ticket? ticket = _adminService.GetTicket(ticketId);

            if (ticket == null)
            {
                _ui.PrintError("Ticket not found.");
                _ui.Pause();
                return;
            }

            if (!_ui.Confirm($"Delete ticket #{ticket.Id} '{ticket.Title}' and its comments?"))
            {
                _ui.PrintInfo("Deletion cancelled.");
                _ui.Pause();
                return;
            }

            if (_adminService.TryDeleteTicket(ticket.Id, out string error))
                _ui.PrintSuccess("Ticket deleted.");
            else
                _ui.PrintError(error);

            _ui.Pause();
        }
    }
}

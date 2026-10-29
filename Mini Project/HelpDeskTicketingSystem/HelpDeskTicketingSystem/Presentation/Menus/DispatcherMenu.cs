using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Interfaces.Tickets;
using HelpDeskTicketingSystem.Presentation.Helpers;
using HelpDeskTicketingSystem.Presentation.UI;

namespace HelpDeskTicketingSystem.Presentation.Menus
{
    internal class DispatcherMenu : IRoleMenu
    {
        private readonly IConsoleUI _ui;
        private readonly IDispatcherService _dispatcherService;
        private readonly ICommentPermissionService _commentPermission;
        private readonly TicketDisplayHelper _ticketDisplay;

        public DispatcherMenu(
            IConsoleUI ui,
            IDispatcherService dispatcherService,
            ICommentPermissionService commentPermission,
            TicketDisplayHelper ticketDisplay)
        {
            _ui = ui;
            _dispatcherService = dispatcherService;
            _commentPermission = commentPermission;
            _ticketDisplay = ticketDisplay;
        }

        public Role Role => Role.Dispatcher;

        public void Show(User user)
        {
            while (true)
            {
                _ui.ClearScreen();
                int choice = _ui.PrintMenu($"DISPATCHER MENU - {user.Name}",
                [
                    "Review open tickets",
                    "Work on tickets I own",
                    "Logout"
                ]);

                if (choice == 1)
                {
                    ReviewOpenTickets(user);
                }
                else if (choice == 2)
                {
                    WorkOnOwnTickets(user);
                }
                else
                {
                    _ui.PrintInfo("Logging out...");
                    _ui.Pause();
                    return;
                }
            }
        }

        private void ReviewOpenTickets(User user)
        {
            _ui.ClearScreen();
            _ui.PrintHeader("OPEN TICKETS");

            List<Ticket> tickets = _dispatcherService.GetOpenTickets();

            if (tickets.Count == 0)
            {
                _ui.PrintInfo("No open tickets to review.");
                _ui.Pause();
                return;
            }

            _ticketDisplay.PrintTicketTable(tickets);
            _ui.PrintDivider();

            int ticketId = _ui.ReadInt("Enter ticket ID to process (0 to go back): ");

            if (ticketId == 0)
                return;

            Ticket? ticket = _dispatcherService.GetOpenTicket(ticketId);

            if (ticket == null)
            {
                _ui.PrintError("Ticket not found among open tickets.");
                _ui.Pause();
                return;
            }

            ProcessOpenTicket(user, ticket);
        }

        private void ProcessOpenTicket(User user, Ticket ticket)
        {
            _ui.ClearScreen();
            _ticketDisplay.PrintTicketDetails(ticket);

            int action = _ui.PrintMenu("Triage this ticket",
            [
                "Set priority and route to a department",
                "Set priority and take ownership personally",
                "Cancel"
            ]);

            if (action == 3)
                return;

            TicketPriority priority = ReadPriority();

            if (action == 1)
            {
                Department department = ReadDepartment();

                if (_dispatcherService.TryAssignToDepartment(user.Id, ticket.Id, priority, department, out Agent? agent, out string error))
                    _ui.PrintSuccess($"Ticket #{ticket.Id} assigned to {agent!.Name}. Due: {ticket.DueDate:g}");
                else
                    _ui.PrintError(error);
            }
            else
            {
                if (_dispatcherService.TryTakeOwnership(user.Id, ticket.Id, priority, out string error))
                    _ui.PrintSuccess($"Ticket #{ticket.Id} is now yours. Due: {ticket.DueDate:g}");
                else
                    _ui.PrintError(error);
            }

            _ui.Pause();
        }

        private void WorkOnOwnTickets(User user)
        {
            _ui.ClearScreen();
            _ui.PrintHeader("TICKETS I OWN");

            List<Ticket> tickets = _dispatcherService.GetOwnedTickets(user.Id);

            if (tickets.Count == 0)
            {
                _ui.PrintInfo("You have not taken ownership of any tickets.");
                _ui.Pause();
                return;
            }

            _ticketDisplay.PrintTicketTable(tickets);
            _ui.PrintDivider();

            int ticketId = _ui.ReadInt("Enter ticket ID to work on (0 to go back): ");

            if (ticketId == 0)
                return;

            if (_dispatcherService.GetOwnedTicket(user.Id, ticketId) == null)
            {
                _ui.PrintError("Ticket not found among your tickets.");
                _ui.Pause();
                return;
            }

            WorkOnTicket(user, ticketId);
        }

        private void WorkOnTicket(User user, int ticketId)
        {
            while (true)
            {
                Ticket? ticket = _dispatcherService.GetOwnedTicket(user.Id, ticketId);

                if (ticket == null)
                {
                    _ui.PrintError("Ticket not found.");
                    _ui.Pause();
                    return;
                }

                _ui.ClearScreen();
                _ticketDisplay.PrintTicketDetails(ticket);

                if (ticket.Status == TicketStatus.Closed)
                {
                    _ui.PrintInfo("This ticket is closed. No actions available.");
                    _ui.Pause();
                    return;
                }

                if (ticket.Status == TicketStatus.Resolved)
                    _ui.PrintInfo("Waiting for the client to confirm, or the ticket closes automatically after 3 days.");

                List<string> labels = new List<string>();
                List<string> keys = new List<string>();

                if (ticket.Status == TicketStatus.Assigned)
                {
                    labels.Add("Start progress");
                    keys.Add("start");
                }

                if (_commentPermission.CanComment(ticket, user))
                {
                    labels.Add("Add a comment");
                    keys.Add("comment");
                }

                if (ticket.Status == TicketStatus.InProgress)
                {
                    labels.Add("Mark as resolved");
                    keys.Add("resolve");
                }

                labels.Add("Back");
                keys.Add("back");

                int choice = _ui.PrintMenu($"Ticket #{ticket.Id} - actions", labels.ToArray());
                string key = keys[choice - 1];

                if (key == "start")
                {
                    if (_dispatcherService.TryStartProgress(user.Id, ticket.Id, out string error))
                        _ui.PrintSuccess("Ticket moved to In Progress.");
                    else
                        _ui.PrintError(error);

                    _ui.Pause();
                }
                else if (key == "comment")
                {
                    string text = _ui.ReadText("Your comment: ");

                    if (_dispatcherService.TryAddComment(user.Id, ticket.Id, text, out string error))
                        _ui.PrintSuccess("Comment added.");
                    else
                        _ui.PrintError(error);

                    _ui.Pause();
                }
                else if (key == "resolve")
                {
                    if (_dispatcherService.TryResolveTicket(user.Id, ticket.Id, out string error))
                        _ui.PrintSuccess("Ticket marked as Resolved.");
                    else
                        _ui.PrintError(error);

                    _ui.Pause();
                }
                else
                {
                    return;
                }
            }
        }

        private TicketPriority ReadPriority()
        {
            TicketPriority[] priorities = Enum.GetValues<TicketPriority>();
            int choice = _ui.PrintMenu("Select priority", priorities.Select(p => p.ToString()).ToArray());
            return priorities[choice - 1];
        }

        private Department ReadDepartment()
        {
            Department[] departments = Enum.GetValues<Department>()
                .Where(d => d != Department.GeneralQuestions)
                .ToArray();

            int choice = _ui.PrintMenu("Select department", departments.Select(d => d.ToString()).ToArray());
            return departments[choice - 1];
        }
    }
}

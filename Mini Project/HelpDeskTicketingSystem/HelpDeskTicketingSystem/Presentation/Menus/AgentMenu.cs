using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Interfaces.Tickets;
using HelpDeskTicketingSystem.Presentation.Helpers;
using HelpDeskTicketingSystem.Presentation.UI;

namespace HelpDeskTicketingSystem.Presentation.Menus
{
    internal class AgentMenu : IRoleMenu
    {
        private readonly IConsoleUI _ui;
        private readonly IAgentService _agentService;
        private readonly ICommentPermissionService _commentPermission;
        private readonly TicketDisplayHelper _ticketDisplay;

        public AgentMenu(
            IConsoleUI ui,
            IAgentService agentService,
            ICommentPermissionService commentPermission,
            TicketDisplayHelper ticketDisplay)
        {
            _ui = ui;
            _agentService = agentService;
            _commentPermission = commentPermission;
            _ticketDisplay = ticketDisplay;
        }

        public Role Role => Role.Agent;

        public void Show(User user)
        {
            while (true)
            {
                _ui.ClearScreen();
                int choice = _ui.PrintMenu($"AGENT MENU - {user.Name}",
                [
                    "My assigned tickets",
                    "My closed tickets",
                    "Logout"
                ]);

                if (choice == 1)
                {
                    ViewAssignedTickets(user);
                }
                else if (choice == 2)
                {
                    ViewClosedTickets(user);
                }
                else
                {
                    _ui.PrintInfo("Logging out...");
                    _ui.Pause();
                    return;
                }
            }
        }

        private void ViewAssignedTickets(User user)
        {
            _ui.ClearScreen();
            _ui.PrintHeader("MY ASSIGNED TICKETS");

            List<Ticket> tickets = _agentService.GetActiveTickets(user.Id);

            if (tickets.Count == 0)
            {
                _ui.PrintInfo("You have no active tickets.");
                _ui.Pause();
                return;
            }

            _ticketDisplay.PrintTicketTable(tickets);
            _ui.PrintDivider();

            int ticketId = _ui.ReadInt("Enter ticket ID to open (0 to go back): ");

            if (ticketId == 0)
                return;

            if (_agentService.GetAssignedTicket(user.Id, ticketId) == null)
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
                Ticket? ticket = _agentService.GetAssignedTicket(user.Id, ticketId);

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
                    if (_agentService.TryStartProgress(user.Id, ticket.Id, out string error))
                        _ui.PrintSuccess("Ticket moved to In Progress.");
                    else
                        _ui.PrintError(error);

                    _ui.Pause();
                }
                else if (key == "comment")
                {
                    string text = _ui.ReadText("Your comment: ");

                    if (_agentService.TryAddComment(user.Id, ticket.Id, text, out string error))
                        _ui.PrintSuccess("Comment added.");
                    else
                        _ui.PrintError(error);

                    _ui.Pause();
                }
                else if (key == "resolve")
                {
                    if (_agentService.TryResolveTicket(user.Id, ticket.Id, out string error))
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

        private void ViewClosedTickets(User user)
        {
            _ui.ClearScreen();
            _ui.PrintHeader("MY CLOSED TICKETS");

            List<Ticket> tickets = _agentService.GetClosedTickets(user.Id);

            if (tickets.Count == 0)
            {
                _ui.PrintInfo("You have no closed tickets.");
                _ui.Pause();
                return;
            }

            _ticketDisplay.PrintTicketTable(tickets);
            _ui.PrintDivider();

            int ticketId = _ui.ReadInt("Enter ticket ID to view (0 to go back): ");

            if (ticketId == 0)
                return;

            Ticket? ticket = _agentService.GetAssignedTicket(user.Id, ticketId);

            if (ticket == null || ticket.Status != TicketStatus.Closed)
            {
                _ui.PrintError("Ticket not found among your closed tickets.");
                _ui.Pause();
                return;
            }

            _ui.ClearScreen();
            _ticketDisplay.PrintTicketDetails(ticket);
            _ui.PrintInfo("This ticket is closed. No actions available.");
            _ui.Pause();
        }
    }
}

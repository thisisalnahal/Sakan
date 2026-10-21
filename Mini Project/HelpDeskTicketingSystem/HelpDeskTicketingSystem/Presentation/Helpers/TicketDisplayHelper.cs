using HelpDeskTicketingSystem.Domain.Entities;

namespace HelpDeskTicketingSystem.Presentation
{
    internal class TicketDisplayHelper
    {
        private readonly IConsoleUI _ui;

        public TicketDisplayHelper(IConsoleUI ui)
        {
            _ui = ui;
        }

        public void PrintTicketTable(List<Ticket> tickets)
        {
            string[] headers = { "ID", "Title", "Priority", "Department", "Status", "Due", "Overdue" };
            List<string[]> rows = new List<string[]>();
            ConsoleColor[] colors = new ConsoleColor[tickets.Count];

            for (int i = 0; i < tickets.Count; i++)
            {
                Ticket ticket = tickets[i];
                bool overdue = ticket.IsOverdue();

                rows.Add(new[]
                {
                    ticket.Id.ToString(),
                    ticket.Title,
                    ticket.Priority?.ToString() ?? "-",
                    ticket.Department?.ToString() ?? "-",
                    ticket.Status.ToString(),
                    ticket.DueDate?.ToString("g") ?? "-",
                    overdue ? "OVERDUE" : "-"
                });

                colors[i] = overdue ? ConsoleColor.Red : ConsoleColor.White;
            }

            _ui.PrintTable(headers, rows, colors);
        }

        public void PrintTicketDetails(Ticket ticket)
        {
            _ui.PrintHeader($"Ticket #{ticket.Id}");
            _ui.PrintInfo($"Title: {ticket.Title}");
            _ui.PrintInfo($"Description: {ticket.Description}");
            _ui.PrintInfo($"Status: {ticket.Status}");
            _ui.PrintInfo($"Priority: {ticket.Priority?.ToString() ?? "-"}");
            _ui.PrintInfo($"Department: {ticket.Department?.ToString() ?? "-"}");
            _ui.PrintInfo($"Client: {ticket.Client?.Name ?? "-"}");
            _ui.PrintInfo($"Assigned to: {ticket.AssignedUser?.Name ?? "-"}");
            _ui.PrintInfo($"Created: {ticket.CreatedAt:yyyy-MM-dd HH:mm}");
            _ui.PrintInfo($"Due: {ticket.DueDate?.ToString("g") ?? "-"}");

            if (ticket.IsOverdue())
                _ui.PrintWarning("This ticket is OVERDUE.");

            _ui.PrintDivider();
            PrintComments(ticket);
            _ui.PrintDivider();
        }

        public void PrintComments(Ticket ticket)
        {
            _ui.PrintInfo("Comments:");

            if (ticket.Comments.Count == 0)
            {
                _ui.PrintInfo("  (no comments yet)");
                return;
            }

            foreach (Comment comment in ticket.Comments.OrderBy(c => c.CreatedAt))
            {
                string name = comment.Author?.Name ?? "Unknown";
                string role = comment.Author?.Role.ToString() ?? "Unknown";

                _ui.PrintInfo($"[{role}] {name} - {comment.CreatedAt:yyyy-MM-dd HH:mm}");
                _ui.PrintInfo($"    {comment.Text}");
            }
        }
    }
}
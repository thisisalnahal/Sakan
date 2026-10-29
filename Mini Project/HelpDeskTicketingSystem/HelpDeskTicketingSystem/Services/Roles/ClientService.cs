using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Roles
{
    internal class ClientService : IClientService
    {
        private readonly DataStore _store;
        private readonly ITicketStatusService _statusService;
        private readonly ICommentPermissionService _commentPermission;

        public ClientService(DataStore store, ITicketStatusService statusService, ICommentPermissionService commentPermission)
        {
            _store = store;
            _statusService = statusService;
            _commentPermission = commentPermission;
        }

        public Ticket CreateTicket(int clientId, string title, string description)
        {
            Ticket ticket = new Ticket
            {
                Id = _store.NextTicketId(),
                Title = title.Trim(),
                Description = description.Trim(),
                Status = TicketStatus.Open,
                ClientId = clientId,
                CreatedAt = DateTime.Now
            };

            _store.Tickets.Add(ticket);
            return ticket;
        }

        public List<Ticket> GetMyTickets(int clientId)
        {
            return _store.Tickets
                .Where(t => t.ClientId == clientId)
                .OrderBy(t => t.Id)
                .ToList();
        }

        public Ticket? GetMyTicket(int clientId, int ticketId)
        {
            return _store.Tickets.FirstOrDefault(t => t.Id == ticketId && t.ClientId == clientId);
        }

        public bool TryAddComment(int clientId, int ticketId, string text, out string error)
        {
            error = string.Empty;

            User? client = FindUser(clientId);
            Ticket? ticket = GetMyTicket(clientId, ticketId);

            if (client == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            if (!_commentPermission.CanComment(ticket, client))
            {
                error = "You cannot comment on a closed ticket.";
                return false;
            }

            ticket.Comments.Add(new Comment
            {
                Id = _store.NextCommentId(),
                TicketId = ticket.Id,
                AuthorId = clientId,
                Text = text.Trim(),
                CreatedAt = DateTime.Now
            });

            return true;
        }

        public bool TryCloseTicket(int clientId, int ticketId, out string error)
        {
            error = string.Empty;

            User? client = FindUser(clientId);
            Ticket? ticket = GetMyTicket(clientId, ticketId);

            if (client == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            return _statusService.TryTransition(ticket, TicketStatus.Closed, client, out error);
        }

        public bool TryUpdateName(int clientId, string newName, out string error)
        {
            error = string.Empty;

            User? client = FindUser(clientId);

            if (client == null)
            {
                error = "Account not found.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(newName))
            {
                error = "Name cannot be empty.";
                return false;
            }

            client.Name = newName.Trim();
            return true;
        }

        public bool TryUpdateEmail(int clientId, string newEmail, out string error)
        {
            error = string.Empty;

            User? client = FindUser(clientId);

            if (client == null)
            {
                error = "Account not found.";
                return false;
            }

            string normalizedEmail = newEmail.Trim();

            bool taken = _store.Users.Any(u =>
                u.Id != clientId
                && string.Equals(u.Email, normalizedEmail, StringComparison.OrdinalIgnoreCase));

            if (taken)
            {
                error = "Another account already uses this email.";
                return false;
            }

            client.Email = normalizedEmail;
            return true;
        }

        private User? FindUser(int userId)
        {
            return _store.Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}

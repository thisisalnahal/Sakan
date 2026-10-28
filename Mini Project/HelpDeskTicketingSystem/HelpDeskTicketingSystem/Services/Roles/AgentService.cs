using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Roles
{
    internal class AgentService : IAgentService
    {
        private readonly DataStore _store;
        private readonly ITicketStatusService _statusService;
        private readonly ICommentPermissionService _commentPermission;

        public AgentService(DataStore store, ITicketStatusService statusService, ICommentPermissionService commentPermission)
        {
            _store = store;
            _statusService = statusService;
            _commentPermission = commentPermission;
        }

        public List<Ticket> GetActiveTickets(int agentId)
        {
            return _store.Tickets
                .Where(t => t.AssignedUserId == agentId && t.Status != TicketStatus.Closed)
                .OrderBy(t => t.Id)
                .ToList();
        }

        public List<Ticket> GetClosedTickets(int agentId)
        {
            return _store.Tickets
                .Where(t => t.AssignedUserId == agentId && t.Status == TicketStatus.Closed)
                .OrderBy(t => t.Id)
                .ToList();
        }

        public Ticket? GetAssignedTicket(int agentId, int ticketId)
        {
            return _store.Tickets.FirstOrDefault(t => t.Id == ticketId && t.AssignedUserId == agentId);
        }

        public bool TryStartProgress(int agentId, int ticketId, out string error)
        {
            error = string.Empty;

            User? agent = FindUser(agentId);
            Ticket? ticket = GetAssignedTicket(agentId, ticketId);

            if (agent == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            return _statusService.TryTransition(ticket, TicketStatus.InProgress, agent, out error);
        }

        public bool TryAddComment(int agentId, int ticketId, string text, out string error)
        {
            error = string.Empty;

            User? agent = FindUser(agentId);
            Ticket? ticket = GetAssignedTicket(agentId, ticketId);

            if (agent == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            if (!_commentPermission.CanComment(ticket, agent))
            {
                error = "You can comment only after starting progress, and never on a closed ticket.";
                return false;
            }

            ticket.Comments.Add(new Comment
            {
                Id = _store.NextCommentId(),
                TicketId = ticket.Id,
                AuthorId = agentId,
                Text = text.Trim(),
                CreatedAt = DateTime.Now
            });

            return true;
        }

        public bool TryResolveTicket(int agentId, int ticketId, out string error)
        {
            error = string.Empty;

            User? agent = FindUser(agentId);
            Ticket? ticket = GetAssignedTicket(agentId, ticketId);

            if (agent == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            return _statusService.TryTransition(ticket, TicketStatus.Resolved, agent, out error);
        }

        private User? FindUser(int userId)
        {
            return _store.Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}

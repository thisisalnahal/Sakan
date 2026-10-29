using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Roles
{
    internal class DispatcherService : IDispatcherService
    {
        private readonly DataStore _store;
        private readonly ITicketStatusService _statusService;
        private readonly ICommentPermissionService _commentPermission;
        private readonly IAssignmentService _assignmentService;
        private readonly IDueDateCalculator _dueDateCalculator;

        public DispatcherService(
            DataStore store,
            ITicketStatusService statusService,
            ICommentPermissionService commentPermission,
            IAssignmentService assignmentService,
            IDueDateCalculator dueDateCalculator)
        {
            _store = store;
            _statusService = statusService;
            _commentPermission = commentPermission;
            _assignmentService = assignmentService;
            _dueDateCalculator = dueDateCalculator;
        }

        public List<Ticket> GetOpenTickets()
        {
            return _store.Tickets
                .Where(t => t.Status == TicketStatus.Open)
                .OrderBy(t => t.CreatedAt)
                .ToList();
        }

        public Ticket? GetOpenTicket(int ticketId)
        {
            return _store.Tickets.FirstOrDefault(t => t.Id == ticketId && t.Status == TicketStatus.Open);
        }

        public bool TryAssignToDepartment(int dispatcherId, int ticketId, TicketPriority priority, Department department, out Agent? assignedAgent, out string error)
        {
            assignedAgent = null;
            error = string.Empty;

            User? dispatcher = FindUser(dispatcherId);
            Ticket? ticket = GetOpenTicket(ticketId);

            if (dispatcher == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            if (department == Department.GeneralQuestions)
            {
                error = "General questions are handled by taking the ticket personally.";
                return false;
            }

            Agent? agent = _assignmentService.GetLeastBusyAgent(department);

            if (agent == null)
            {
                error = $"No agents are available in the {department} department. Nothing was saved.";
                return false;
            }

            if (!_statusService.CanTransition(ticket, TicketStatus.Assigned, dispatcher, out error))
                return false;

            DateTime now = DateTime.Now;

            ticket.Priority = priority;
            ticket.Department = department;
            ticket.DueDate = _dueDateCalculator.Calculate(priority, now);
            ticket.AssignedUserId = agent.Id;
            agent.LastAssignedAt = now;

            if (!_statusService.TryTransition(ticket, TicketStatus.Assigned, dispatcher, out error))
                return false;

            assignedAgent = agent;
            return true;
        }

        public bool TryTakeOwnership(int dispatcherId, int ticketId, TicketPriority priority, out string error)
        {
            error = string.Empty;

            User? dispatcher = FindUser(dispatcherId);
            Ticket? ticket = GetOpenTicket(ticketId);

            if (dispatcher == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            if (!_statusService.CanTransition(ticket, TicketStatus.Assigned, dispatcher, out error))
                return false;

            ticket.Priority = priority;
            ticket.Department = Department.GeneralQuestions;
            ticket.DueDate = _dueDateCalculator.Calculate(priority, DateTime.Now);
            ticket.AssignedUserId = dispatcher.Id;

            return _statusService.TryTransition(ticket, TicketStatus.Assigned, dispatcher, out error);
        }

        public List<Ticket> GetOwnedTickets(int dispatcherId)
        {
            return _store.Tickets
                .Where(t => t.AssignedUserId == dispatcherId)
                .OrderBy(t => t.Id)
                .ToList();
        }

        public Ticket? GetOwnedTicket(int dispatcherId, int ticketId)
        {
            return _store.Tickets.FirstOrDefault(t => t.Id == ticketId && t.AssignedUserId == dispatcherId);
        }

        public bool TryStartProgress(int dispatcherId, int ticketId, out string error)
        {
            error = string.Empty;

            User? dispatcher = FindUser(dispatcherId);
            Ticket? ticket = GetOwnedTicket(dispatcherId, ticketId);

            if (dispatcher == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            return _statusService.TryTransition(ticket, TicketStatus.InProgress, dispatcher, out error);
        }

        public bool TryAddComment(int dispatcherId, int ticketId, string text, out string error)
        {
            error = string.Empty;

            User? dispatcher = FindUser(dispatcherId);
            Ticket? ticket = GetOwnedTicket(dispatcherId, ticketId);

            if (dispatcher == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            if (!_commentPermission.CanComment(ticket, dispatcher))
            {
                error = "You can comment only after starting progress, and never on a closed ticket.";
                return false;
            }

            ticket.Comments.Add(new Comment
            {
                Id = _store.NextCommentId(),
                TicketId = ticket.Id,
                AuthorId = dispatcherId,
                Text = text.Trim(),
                CreatedAt = DateTime.Now
            });

            return true;
        }

        public bool TryResolveTicket(int dispatcherId, int ticketId, out string error)
        {
            error = string.Empty;

            User? dispatcher = FindUser(dispatcherId);
            Ticket? ticket = GetOwnedTicket(dispatcherId, ticketId);

            if (dispatcher == null || ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            return _statusService.TryTransition(ticket, TicketStatus.Resolved, dispatcher, out error);
        }

        private User? FindUser(int userId)
        {
            return _store.Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}

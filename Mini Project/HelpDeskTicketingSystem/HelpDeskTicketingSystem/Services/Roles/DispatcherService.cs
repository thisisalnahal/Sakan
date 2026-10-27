using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Roles
{
    internal class DispatcherService : IDispatcherService
    {
        public Ticket? GetOpenTicket(int ticketId)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> GetOpenTickets()
        {
            throw new NotImplementedException();
        }

        public Ticket? GetOwnedTicket(int dispatcherId, int ticketId)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> GetOwnedTickets(int dispatcherId)
        {
            throw new NotImplementedException();
        }

        public bool TryAddComment(int dispatcherId, int ticketId, string text, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryAssignToDepartment(int dispatcherId, int ticketId, TicketPriority priority, Department department, out Agent? assignedAgent, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryResolveTicket(int dispatcherId, int ticketId, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryStartProgress(int dispatcherId, int ticketId, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryTakeOwnership(int dispatcherId, int ticketId, TicketPriority priority, out string error)
        {
            throw new NotImplementedException();
        }
    }
}
using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface IDispatcherService
    {
        List<Ticket> GetOpenTickets();

        Ticket? GetOpenTicket(int ticketId);

        bool TryAssignToDepartment(int dispatcherId, int ticketId, TicketPriority priority, Department department, out Agent? assignedAgent, out string error);

        bool TryTakeOwnership(int dispatcherId, int ticketId, TicketPriority priority, out string error);

        List<Ticket> GetOwnedTickets(int dispatcherId);

        Ticket? GetOwnedTicket(int dispatcherId, int ticketId);

        bool TryStartProgress(int dispatcherId, int ticketId, out string error);

        bool TryAddComment(int dispatcherId, int ticketId, string text, out string error);

        bool TryResolveTicket(int dispatcherId, int ticketId, out string error);
    }
}
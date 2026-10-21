using HelpDeskTicketingSystem.Domain.Entities;

namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface IAgentService
    {
        List<Ticket> GetActiveTickets(int agentId);

        List<Ticket> GetClosedTickets(int agentId);

        Ticket? GetAssignedTicket(int agentId, int ticketId);

        bool TryStartProgress(int agentId, int ticketId, out string error);

        bool TryAddComment(int agentId, int ticketId, string text, out string error);

        bool TryResolveTicket(int agentId, int ticketId, out string error);
    }
}
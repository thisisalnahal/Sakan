using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Roles
{
    internal class AgentService : IAgentService
    {
        public List<Ticket> GetActiveTickets(int agentId)
        {
            throw new NotImplementedException();
        }

        public Ticket? GetAssignedTicket(int agentId, int ticketId)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> GetClosedTickets(int agentId)
        {
            throw new NotImplementedException();
        }

        public bool TryAddComment(int agentId, int ticketId, string text, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryResolveTicket(int agentId, int ticketId, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryStartProgress(int agentId, int ticketId, out string error)
        {
            throw new NotImplementedException();
        }
    }
}
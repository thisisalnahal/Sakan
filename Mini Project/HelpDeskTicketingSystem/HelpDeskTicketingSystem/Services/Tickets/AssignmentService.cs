using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Tickets
{
    internal class AssignmentService : IAssignmentService
    {
        private readonly DataStore _store;

        public AssignmentService(DataStore store)
        {
            _store = store;
        }

        public Agent? GetLeastBusyAgent(Department department)
        {
            return _store.Users
                .OfType<Agent>()
                .Where(a => a.Department == department)
                .OrderBy(a => GetActiveTicketCount(a.Id))
                .ThenBy(a => a.LastAssignedAt ?? DateTime.MinValue)
                .FirstOrDefault();
        }

        private int GetActiveTicketCount(int agentId)
        {
            return _store.Tickets.Count(t =>
                t.AssignedUserId == agentId
                && t.Status != TicketStatus.Resolved
                && t.Status != TicketStatus.Closed);
        }
    }
}

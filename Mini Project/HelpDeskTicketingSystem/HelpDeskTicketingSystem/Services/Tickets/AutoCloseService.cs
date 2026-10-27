using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Tickets
{
    internal class AutoCloseService : IAutoCloseService
    {
        private readonly DataStore _store;
        private readonly ITicketStatusService _statusService;

        public AutoCloseService(DataStore store, ITicketStatusService statusService)
        {
            _store = store;
            _statusService = statusService;
        }

        public int CloseExpiredResolvedTickets()
        {
            DateTime threshold = DateTime.Now.AddDays(-3);

            List<Ticket> expiredTickets = _store.Tickets
                .Where(t => t.Status == TicketStatus.Resolved
                         && t.ResolvedAt != null
                         && t.ResolvedAt <= threshold)
                .ToList();

            int closedCount = 0;

            foreach (Ticket ticket in expiredTickets)
            {
                if (_statusService.TryTransition(ticket, TicketStatus.Closed, null, out _, isSystemAction: true))
                    closedCount++;
            }

            return closedCount;
        }
    }
}
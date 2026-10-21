using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface ITicketStatusService
    {
        bool CanTransition(Ticket ticket, TicketStatus newStatus, User? actor, out string error, bool isSystemAction = false);

        bool TryTransition(Ticket ticket, TicketStatus newStatus, User? actor, out string error, bool isSystemAction = false);
    }
}
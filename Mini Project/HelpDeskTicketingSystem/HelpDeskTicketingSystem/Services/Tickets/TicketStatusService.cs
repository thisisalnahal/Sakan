using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Tickets

{
    internal class TicketStatusService : ITicketStatusService
    {
        public bool CanTransition(Ticket ticket, TicketStatus newStatus, User? actor, out string error, bool isSystemAction = false)
        {
            error = string.Empty;

            if (ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            switch (ticket.Status)
            {
                case TicketStatus.Open:
                    if (newStatus != TicketStatus.Assigned)
                    {
                        error = "Open tickets can only be assigned.";
                        return false;
                    }

                    if (!isSystemAction && (actor == null || actor.Role != Role.Dispatcher))
                    {
                        error = "Only a Dispatcher can assign a ticket.";
                        return false;
                    }
                    break;

                case TicketStatus.Assigned:
                    if (newStatus != TicketStatus.InProgress)
                    {
                        error = "Assigned tickets can only move to InProgress.";
                        return false;
                    }

                    if (!isSystemAction && (actor == null || ticket.AssignedUserId != actor.Id))
                    {
                        error = "Only the assigned owner can start progress.";
                        return false;
                    }
                    break;

                case TicketStatus.InProgress:
                    if (newStatus != TicketStatus.Resolved)
                    {
                        error = "InProgress tickets can only be resolved.";
                        return false;
                    }

                    if (!isSystemAction && (actor == null || ticket.AssignedUserId != actor.Id))
                    {
                        error = "Only the assigned owner can resolve the ticket.";
                        return false;
                    }
                    break;

                case TicketStatus.Resolved:
                    if (newStatus != TicketStatus.Closed)
                    {
                        error = "Resolved tickets can only be closed.";
                        return false;
                    }

                    if (!isSystemAction && (actor == null || actor.Role != Role.Client || ticket.ClientId != actor.Id))
                    {
                        error = "Only the client who submitted this ticket can close it.";
                        return false;
                    }
                    break;

                case TicketStatus.Closed:
                    error = "Closed tickets cannot be changed.";
                    return false;
            }

            return true;
        }

        public bool TryTransition(Ticket ticket, TicketStatus newStatus, User? actor, out string error, bool isSystemAction = false)
        {
            if (!CanTransition(ticket, newStatus, actor, out error, isSystemAction))
                return false;

            ticket.Status = newStatus;

            if (newStatus == TicketStatus.Resolved)
                ticket.ResolvedAt = DateTime.Now;

            return true;
        }
    }
}
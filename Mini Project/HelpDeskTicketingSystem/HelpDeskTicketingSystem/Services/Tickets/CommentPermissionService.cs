using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Interfaces;

namespace HelpDeskTicketingSystem.Services
{
    internal class CommentPermissionService : ICommentPermissionService
    {
        public bool CanComment(Ticket ticket, User user)
        {
            if (ticket == null || user == null)
                return false;

            if (ticket.Status == TicketStatus.Closed)
                return false;

            if (ticket.ClientId == user.Id)
                return true;

            bool isOwner = ticket.AssignedUserId == user.Id;
            bool hasStartedProgress = ticket.Status == TicketStatus.InProgress
                                   || ticket.Status == TicketStatus.Resolved;

            return isOwner && hasStartedProgress;
        }
    }
}
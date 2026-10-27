using HelpDeskTicketingSystem.Domain.Entities;

namespace HelpDeskTicketingSystem.Interfaces.Tickets
{
    internal interface ICommentPermissionService
    {
        bool CanComment(Ticket ticket, User user);
    }
}
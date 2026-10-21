using HelpDeskTicketingSystem.Domain.Entities;

namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface ICommentPermissionService
    {
        bool CanComment(Ticket ticket, User user);
    }
}
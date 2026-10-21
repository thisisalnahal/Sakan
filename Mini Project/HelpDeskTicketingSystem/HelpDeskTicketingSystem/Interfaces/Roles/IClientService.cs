using HelpDeskTicketingSystem.Domain.Entities;

namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface IClientService
    {
        Ticket CreateTicket(int clientId, string title, string description);

        List<Ticket> GetMyTickets(int clientId);

        Ticket? GetMyTicket(int clientId, int ticketId);

        bool TryAddComment(int clientId, int ticketId, string text, out string error);

        bool TryCloseTicket(int clientId, int ticketId, out string error);

        bool TryUpdateName(int clientId, string newName, out string error);

        bool TryUpdateEmail(int clientId, string newEmail, out string error);
    }
}
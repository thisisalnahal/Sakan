using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Roles;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Roles
{
    internal class ClientService : IClientService
    {
        public Ticket CreateTicket(int clientId, string title, string description)
        {
            throw new NotImplementedException();
        }

        public Ticket? GetMyTicket(int clientId, int ticketId)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> GetMyTickets(int clientId)
        {
            throw new NotImplementedException();
        }

        public bool TryAddComment(int clientId, int ticketId, string text, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryCloseTicket(int clientId, int ticketId, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdateEmail(int clientId, string newEmail, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdateName(int clientId, string newName, out string error)
        {
            throw new NotImplementedException();
        }
    }
}
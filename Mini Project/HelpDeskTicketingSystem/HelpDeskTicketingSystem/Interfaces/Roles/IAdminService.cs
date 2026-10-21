using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface IAdminService
    {
        bool TryCreateAgent(string name, string email, string password, Department department, out string error);

        bool TryCreateDispatcher(string name, string email, string password, out string error);

        List<User> GetUsers(Role? role = null);

        User? GetUserByEmail(string email);

        bool TryDeleteUser(int adminId, int targetUserId, out string error);

        List<Ticket> GetTickets(TicketStatus? status = null, Department? department = null);

        Ticket? GetTicket(int ticketId);

        bool TryDeleteTicket(int ticketId, out string error);
    }
}
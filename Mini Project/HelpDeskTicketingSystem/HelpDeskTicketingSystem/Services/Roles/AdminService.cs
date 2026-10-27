using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Auth;
using HelpDeskTicketingSystem.Interfaces.Roles;

namespace HelpDeskTicketingSystem.Services.Roles
{
    internal class AdminService : IAdminService
    {
        public Ticket? GetTicket(int ticketId)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> GetTickets(TicketStatus? status = null, Department? department = null)
        {
            throw new NotImplementedException();
        }

        public User? GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers(Role? role = null)
        {
            throw new NotImplementedException();
        }

        public bool TryCreateAgent(string name, string email, string password, Department department, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryCreateDispatcher(string name, string email, string password, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryDeleteTicket(int ticketId, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryDeleteUser(int adminId, int targetUserId, out string error)
        {
            throw new NotImplementedException();
        }
    }
}
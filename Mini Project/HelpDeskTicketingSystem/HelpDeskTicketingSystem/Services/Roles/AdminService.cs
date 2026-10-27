using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Auth;
using HelpDeskTicketingSystem.Interfaces.Roles;

namespace HelpDeskTicketingSystem.Services.Roles
{
    internal class AdminService : IAdminService
    {
        private readonly DataStore _store;
        private readonly IPasswordHasher _hasher;

        public AdminService(DataStore store, IPasswordHasher hasher)
        {
            _store = store;
            _hasher = hasher;
        }

        public bool TryCreateAgent(string name, string email, string password, Department department, out string error)
        {
            error = string.Empty;

            if (department == Department.GeneralQuestions)
            {
                error = "An agent must belong to Hardware, Software, Network or Billing.";
                return false;
            }

            if (EmailExists(email))
            {
                error = "An account with this email already exists.";
                return false;
            }

            _store.Users.Add(new Agent
            {
                Id = _store.NextUserId(),
                Name = name.Trim(),
                Email = email.Trim(),
                PasswordHash = _hasher.Hash(password),
                MustChangePassword = true,
                Department = department
            });

            return true;
        }

        public bool TryCreateDispatcher(string name, string email, string password, out string error)
        {
            error = string.Empty;

            if (EmailExists(email))
            {
                error = "An account with this email already exists.";
                return false;
            }

            _store.Users.Add(new Dispatcher
            {
                Id = _store.NextUserId(),
                Name = name.Trim(),
                Email = email.Trim(),
                PasswordHash = _hasher.Hash(password),
                MustChangePassword = true
            });

            return true;
        }

        public List<User> GetUsers(Role? role = null)
        {
            return _store.Users
                .Where(u => role == null || u.Role == role)
                .OrderBy(u => u.Id)
                .ToList();
        }

        public User? GetUserByEmail(string email)
        {
            string normalizedEmail = email.Trim();

            return _store.Users.FirstOrDefault(u =>
                string.Equals(u.Email, normalizedEmail, StringComparison.OrdinalIgnoreCase));
        }

        public bool TryDeleteUser(int adminId, int targetUserId, out string error)
        {
            error = string.Empty;

            User? target = _store.Users.FirstOrDefault(u => u.Id == targetUserId);

            if (target == null)
            {
                error = "User not found.";
                return false;
            }

            if (target.Id == adminId)
            {
                error = "You cannot delete your own account.";
                return false;
            }

            bool linkedToTickets = _store.Tickets.Any(t =>
                t.ClientId == target.Id || t.AssignedUserId == target.Id);

            if (linkedToTickets)
            {
                error = "This account is still linked to tickets and cannot be deleted.";
                return false;
            }

            _store.Users.Remove(target);
            return true;
        }

        public List<Ticket> GetTickets(TicketStatus? status = null, Department? department = null)
        {
            return _store.Tickets
                .Where(t => (status == null || t.Status == status)
                         && (department == null || t.Department == department))
                .OrderBy(t => t.Id)
                .ToList();
        }

        public Ticket? GetTicket(int ticketId)
        {
            return _store.Tickets.FirstOrDefault(t => t.Id == ticketId);
        }

        public bool TryDeleteTicket(int ticketId, out string error)
        {
            error = string.Empty;

            Ticket? ticket = GetTicket(ticketId);

            if (ticket == null)
            {
                error = "Ticket not found.";
                return false;
            }

            _store.Tickets.Remove(ticket);
            return true;
        }

        private bool EmailExists(string email)
        {
            return GetUserByEmail(email) != null;
        }
    }
}

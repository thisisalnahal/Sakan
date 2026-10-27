using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Roles;

namespace HelpDeskTicketingSystem.Services.Auth
{
    internal class UserDirectory : IUserDirectory
    {
        private readonly DataStore _store;

        public UserDirectory(DataStore store)
        {
            _store = store;
        }

        public User? GetById(int userId)
        {
            return _store.Users.FirstOrDefault(u => u.Id == userId);
        }

        public string GetName(int userId)
        {
            return GetById(userId)?.Name ?? "Unknown";
        }
    }
}
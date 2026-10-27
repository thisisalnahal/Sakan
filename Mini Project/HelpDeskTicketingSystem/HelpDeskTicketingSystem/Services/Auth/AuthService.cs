using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Auth;

namespace HelpDeskTicketingSystem.Services.Auth
{
    internal class AuthService : IAuthService
    {
        private readonly DataStore _store;
        private readonly IPasswordHasher _hasher;

        public AuthService(DataStore store, IPasswordHasher hasher)
        {
            _store = store;
            _hasher = hasher;
        }

        public User? Login(string email, string password)
        {
            User? user = FindByEmail(email);

            if (user == null || !_hasher.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public bool TryRegisterClient(string name, string email, string password, out string error)
        {
            error = string.Empty;

            if (FindByEmail(email) != null)
            {
                error = "An account with this email already exists.";
                return false;
            }

            Client client = new Client
            {
                Id = _store.NextUserId(),
                Name = name.Trim(),
                Email = email.Trim(),
                PasswordHash = _hasher.Hash(password),
                MustChangePassword = false
            };

            _store.Users.Add(client);
            return true;
        }

        public void CompleteForcedPasswordChange(int userId, string newPassword)
        {
            User user = _store.Users.FirstOrDefault(u => u.Id == userId)
                ?? throw new ArgumentException("User not found.", nameof(userId));

            user.PasswordHash = _hasher.Hash(newPassword);
            user.MustChangePassword = false;
        }

        public bool TryChangePassword(int userId, string currentPassword, string newPassword, out string error)
        {
            error = string.Empty;

            User? user = _store.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                error = "User not found.";
                return false;
            }

            if (!_hasher.Verify(currentPassword, user.PasswordHash))
            {
                error = "Current password is incorrect.";
                return false;
            }

            user.PasswordHash = _hasher.Hash(newPassword);
            user.MustChangePassword = false;
            return true;
        }

        private User? FindByEmail(string email)
        {
            string normalizedEmail = email.Trim();

            return _store.Users.FirstOrDefault(u =>
                string.Equals(u.Email, normalizedEmail, StringComparison.OrdinalIgnoreCase));
        }
    }
}
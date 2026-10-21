using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces;

namespace HelpDeskTicketingSystem.Services
{
    internal class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly IPasswordHasher _hasher;

        public AuthService(AppDbContext db, IPasswordHasher hasher)
        {
            _db = db;
            _hasher = hasher;
        }

        public User? Login(string email, string password)
        {
            string normalizedEmail = email.Trim().ToLower();

            User? user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);

            if (user == null || !_hasher.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public bool TryRegisterClient(string name, string email, string password, out string error)
        {
            error = string.Empty;
            string normalizedEmail = email.Trim().ToLower();

            if (_db.Users.Any(u => u.Email.ToLower() == normalizedEmail))
            {
                error = "An account with this email already exists.";
                return false;
            }

            Client client = new Client
            {
                Name = name.Trim(),
                Email = email.Trim(),
                PasswordHash = _hasher.Hash(password),
                MustChangePassword = false
            };

            _db.Users.Add(client);
            _db.SaveChanges();
            return true;
        }

        public void CompleteForcedPasswordChange(int userId, string newPassword)
        {
            User user = _db.Users.Find(userId)
                ?? throw new ArgumentException("User not found.", nameof(userId));

            user.PasswordHash = _hasher.Hash(newPassword);
            user.MustChangePassword = false;
            _db.SaveChanges();
        }

        public bool TryChangePassword(int userId, string currentPassword, string newPassword, out string error)
        {
            error = string.Empty;

            User? user = _db.Users.Find(userId);

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
            _db.SaveChanges();
            return true;
        }
    }
}
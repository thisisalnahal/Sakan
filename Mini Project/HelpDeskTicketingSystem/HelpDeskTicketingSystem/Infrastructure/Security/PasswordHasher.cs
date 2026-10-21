using System.Security.Cryptography;
using System.Text;
using HelpDeskTicketingSystem.Interfaces;

namespace HelpDeskTicketingSystem.Infrastructure.Security
{
    internal class PasswordHasher : IPasswordHasher
    {
        public string Hash(string plainTextPassword)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainTextPassword);
            byte[] hashBytes = SHA256.HashData(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        public bool Verify(string plainTextPassword, string storedHash)
        {
            return Hash(plainTextPassword) == storedHash;
        }
    }
}
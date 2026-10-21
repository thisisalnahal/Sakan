using HelpDeskTicketingSystem.Domain.Entities;

namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface IAuthService
    {
        User? Login(string email, string password);

        bool TryRegisterClient(string name, string email, string password, out string error);

        void CompleteForcedPasswordChange(int userId, string newPassword);

        bool TryChangePassword(int userId, string currentPassword, string newPassword, out string error);
    }
}
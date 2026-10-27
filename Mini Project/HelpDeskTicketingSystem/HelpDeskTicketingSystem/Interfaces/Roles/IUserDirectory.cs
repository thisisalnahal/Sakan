using HelpDeskTicketingSystem.Domain.Entities;

namespace HelpDeskTicketingSystem.Interfaces.Roles
{
    internal interface IUserDirectory
    {
        User? GetById(int userId);

        string GetName(int userId);
    }
}
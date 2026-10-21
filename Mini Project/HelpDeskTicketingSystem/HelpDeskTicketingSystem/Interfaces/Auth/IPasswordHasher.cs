namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface IPasswordHasher
    {
        string Hash(string plainTextPassword);
        bool Verify(string plainTextPassword, string storedHash);
    }
}
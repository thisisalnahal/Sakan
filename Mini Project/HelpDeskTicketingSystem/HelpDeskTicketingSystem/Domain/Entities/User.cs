using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Domain.Entities
{
    internal abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool MustChangePassword { get; set; }

        public abstract Role Role { get; }

        public override string ToString() => $"[{Id}] {Name} ({Role}) - {Email}";
    }
}
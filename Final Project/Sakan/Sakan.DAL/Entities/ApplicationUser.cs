using Sakan.DAL.Enums.ApplicationUser;
namespace Sakan.DAL.Entities

{
    public class ApplicationUser
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }

        // Hashed password only. The plain password is never stored.
        public string PasswordHash { get; set; } = string.Empty;

        public Gender? Gender { get; set; }
        public string? ProfileImageKey { get; set; }
        public DateTime? IdentityVerifiedAt { get; set; }
        public ApplicationUser? IdentityVerifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

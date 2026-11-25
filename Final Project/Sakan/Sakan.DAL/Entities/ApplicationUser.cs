using Sakan.DAL.Enums.ApplicationUser;
namespace Sakan.DAL.Entities

{
    internal class ApplicationUser
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public Gender? Gender { get; set; }
        public string PfofileImageKey { get; set; }
        public DateTime? IdentityVerifiedAt { get; set; }
        public ApplicationUser? IdentityVerifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}

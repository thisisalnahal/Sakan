using Sakan.DAL.Enums.IdentityVerification;
using System.ComponentModel.DataAnnotations;

namespace Sakan.DAL.Entities
{
    internal class IdentityVerification
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DocumentType DocumentType { get; set; }
        public string FrontImageKey { get; set; } = string.Empty;
        public string? BackImageKey { get; set; }
        public Status Status { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string? ReviewedById { get; set; }
        public string? RejectionReason { get; set; }

        //nav. prop.
        public ApplicationUser User { get; set; } = null!;
        public ApplicationUser? ReviewedBy { get; set; }
    }
}

namespace Sakan.DAL.Entities
{
    internal class DisputeEvidence
    {
        public int Id { get; set; }
        public int DisputeId { get; set; }
        public string UploadedById { get; set; } 
        public string StorageKey { get; set; } 
        public string FileName { get; set; } 
        public string ContentType { get; set; } 
        public long FileSizeBytes { get; set; }
        public DateTime UploadedAt { get; set; } 

        // Navigation Properties
        public Dispute Dispute { get; set; }
        public ApplicationUser UploadedBy { get; set; } 
    }
}

namespace Sakan.DAL.Entities
{
    public class ListingImage
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public string StorageKey { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; }

        // Navigation Properties
        public Listing Listing { get; set; } = null!;
    }
}
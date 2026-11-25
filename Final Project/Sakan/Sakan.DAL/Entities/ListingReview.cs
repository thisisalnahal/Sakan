namespace Sakan.DAL.Entities
{
    internal class ListingReview
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Booking Booking { get; set; } 
    }
}

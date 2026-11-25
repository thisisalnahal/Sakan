namespace Sakan.DAL.Entities
{
    internal class UserReview
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string ReviewerId { get; set; }
        public string RevieweeId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Booking Booking { get; set; }
        public ApplicationUser Reviewer { get; set; } 
        public ApplicationUser Reviewee { get; set; } 
    }
}

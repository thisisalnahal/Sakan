using Sakan.DAL.Enums.Booking;

namespace Sakan.DAL.Entities
{
    public class BookingStatusHistory
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public BookingStatus? OldStatus { get; set; }
        public BookingStatus NewStatus { get; set; }
        public string? ChangedById { get; set; }
        public string? Reason { get; set; }
        public DateTime ChangedAt { get; set; }

        // Navigation Properties
        public Booking Booking { get; set; } = null!;
        public ApplicationUser? ChangedBy { get; set; }
    }
}
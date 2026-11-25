using Sakan.DAL.Enums.Listing;

namespace Sakan.DAL.Entities
{
    public class Listing
    {
        public int Id { get; set; }
        public int RentalUnitId { get; set; }
        public int CancellationPolicyId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public decimal SecurityDepositAmount { get; set; }
        public int? MinStayDays { get; set; }
        public int? MaxStayDays { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public bool RequiresHostApproval { get; set; }
        public ListingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public RentalUnit RentalUnit { get; set; } = null!;
        public CancellationPolicy CancellationPolicy { get; set; } = null!;
        public ICollection<ListingPrice> Prices { get; set; } = new List<ListingPrice>();
        public ICollection<ListingImage> Images { get; set; } = new List<ListingImage>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<ListingReview> Reviews { get; set; } = new List<ListingReview>();
    }
}
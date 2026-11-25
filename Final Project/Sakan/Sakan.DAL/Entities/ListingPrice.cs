using Sakan.DAL.Enums.ListingPrice;

namespace Sakan.DAL.Entities
{
    public class ListingPrice
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public PricingPeriod PricingPeriod { get; set; }
        public decimal Price { get; set; }

        // Navigation Properties
        public Listing Listing { get; set; } = null!;
    }
}
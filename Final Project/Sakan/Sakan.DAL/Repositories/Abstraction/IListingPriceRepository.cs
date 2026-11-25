using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IListingPriceRepository
    {
        Task<IEnumerable<ListingPrice>> GetAllAsync();
        Task<ListingPrice?> GetByIdAsync(int id);
        Task<IEnumerable<ListingPrice>> GetByListingIdAsync(int listingId);
        Task AddAsync(ListingPrice listingPrice);
        void Update(ListingPrice listingPrice);
        void Delete(ListingPrice listingPrice);
    }
}
using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IListingImageRepository
    {
        Task<IEnumerable<ListingImage>> GetAllAsync();
        Task<ListingImage?> GetByIdAsync(int id);
        Task<IEnumerable<ListingImage>> GetByListingIdAsync(int listingId);
        Task AddAsync(ListingImage listingImage);
        void Update(ListingImage listingImage);
        void Delete(ListingImage listingImage);
    }
}
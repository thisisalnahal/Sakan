using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<IEnumerable<Booking>> GetByGuestIdAsync(string guestId);
        Task<IEnumerable<Booking>> GetByListingIdAsync(int listingId);
        Task AddAsync(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);
    }
}
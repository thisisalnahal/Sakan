using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IBookingStatusHistoryRepository
    {
        Task<IEnumerable<BookingStatusHistory>> GetAllAsync();
        Task<BookingStatusHistory?> GetByIdAsync(int id);
        Task<IEnumerable<BookingStatusHistory>> GetByBookingIdAsync(int bookingId);
        Task AddAsync(BookingStatusHistory history);
    }
}
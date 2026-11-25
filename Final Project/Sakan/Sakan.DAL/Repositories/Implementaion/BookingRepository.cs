using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementation
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Set<Booking>().ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Set<Booking>().FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetByGuestIdAsync(string guestId)
        {
            return await _context.Set<Booking>()
                .Where(b => b.GuestId == guestId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByListingIdAsync(int listingId)
        {
            return await _context.Set<Booking>()
                .Where(b => b.ListingId == listingId)
                .ToListAsync();
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Set<Booking>().AddAsync(booking);
        }

        public void Update(Booking booking)
        {
            _context.Set<Booking>().Update(booking);
        }

        public void Delete(Booking booking)
        {
            _context.Set<Booking>().Remove(booking);
        }
    }
}
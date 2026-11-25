using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementation
{
    public class BookingStatusHistoryRepository : IBookingStatusHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingStatusHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookingStatusHistory>> GetAllAsync()
        {
            return await _context.Set<BookingStatusHistory>().ToListAsync();
        }

        public async Task<BookingStatusHistory?> GetByIdAsync(int id)
        {
            return await _context.Set<BookingStatusHistory>().FindAsync(id);
        }

        public async Task<IEnumerable<BookingStatusHistory>> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Set<BookingStatusHistory>()
                .Where(h => h.BookingId == bookingId)
                .OrderBy(h => h.ChangedAt)
                .ToListAsync();
        }

        public async Task AddAsync(BookingStatusHistory history)
        {
            await _context.Set<BookingStatusHistory>().AddAsync(history);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementation
{
    public class ListingPriceRepository : IListingPriceRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingPriceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListingPrice>> GetAllAsync()
        {
            return await _context.Set<ListingPrice>().ToListAsync();
        }

        public async Task<ListingPrice?> GetByIdAsync(int id)
        {
            return await _context.Set<ListingPrice>().FindAsync(id);
        }

        public async Task<IEnumerable<ListingPrice>> GetByListingIdAsync(int listingId)
        {
            return await _context.Set<ListingPrice>()
                .Where(p => p.ListingId == listingId)
                .ToListAsync();
        }

        public async Task AddAsync(ListingPrice listingPrice)
        {
            await _context.Set<ListingPrice>().AddAsync(listingPrice);
        }

        public void Update(ListingPrice listingPrice)
        {
            _context.Set<ListingPrice>().Update(listingPrice);
        }

        public void Delete(ListingPrice listingPrice)
        {
            _context.Set<ListingPrice>().Remove(listingPrice);
        }
    }
}
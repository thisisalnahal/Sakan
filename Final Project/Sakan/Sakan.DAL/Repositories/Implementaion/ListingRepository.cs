using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementation
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Listing>> GetAllAsync()
        {
            return await _context.Set<Listing>().ToListAsync();
        }

        public async Task<Listing?> GetByIdAsync(int id)
        {
            return await _context.Set<Listing>().FindAsync(id);
        }

        public async Task AddAsync(Listing listing)
        {
            await _context.Set<Listing>().AddAsync(listing);
        }

        public void Update(Listing listing)
        {
            _context.Set<Listing>().Update(listing);
        }

        public void Delete(Listing listing)
        {
            _context.Set<Listing>().Remove(listing);
        }
    }
}
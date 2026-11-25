using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementation
{
    public class ListingImageRepository : IListingImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListingImage>> GetAllAsync()
        {
            return await _context.Set<ListingImage>().ToListAsync();
        }

        public async Task<ListingImage?> GetByIdAsync(int id)
        {
            return await _context.Set<ListingImage>().FindAsync(id);
        }

        public async Task<IEnumerable<ListingImage>> GetByListingIdAsync(int listingId)
        {
            return await _context.Set<ListingImage>()
                .Where(i => i.ListingId == listingId)
                .OrderBy(i => i.DisplayOrder)
                .ToListAsync();
        }

        public async Task AddAsync(ListingImage listingImage)
        {
            await _context.Set<ListingImage>().AddAsync(listingImage);
        }

        public void Update(ListingImage listingImage)
        {
            _context.Set<ListingImage>().Update(listingImage);
        }

        public void Delete(ListingImage listingImage)
        {
            _context.Set<ListingImage>().Remove(listingImage);
        }
    }
}
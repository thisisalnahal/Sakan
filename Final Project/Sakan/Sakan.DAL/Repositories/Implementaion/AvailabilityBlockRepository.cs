using Microsoft.EntityFrameworkCore;
using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementation
{
    public class AvailabilityBlockRepository : IAvailabilityBlockRepository
    {
        private readonly ApplicationDbContext _context;

        public AvailabilityBlockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AvailabilityBlock>> GetAllAsync()
        {
            return await _context.Set<AvailabilityBlock>().ToListAsync();
        }

        public async Task<AvailabilityBlock?> GetByIdAsync(int id)
        {
            return await _context.Set<AvailabilityBlock>().FindAsync(id);
        }

        public async Task<IEnumerable<AvailabilityBlock>> GetByRentalUnitIdAsync(int rentalUnitId)
        {
            return await _context.Set<AvailabilityBlock>()
                .Where(a => a.RentalUnitId == rentalUnitId)
                .ToListAsync();
        }

        public async Task AddAsync(AvailabilityBlock block)
        {
            await _context.Set<AvailabilityBlock>().AddAsync(block);
        }

        public void Update(AvailabilityBlock block)
        {
            _context.Set<AvailabilityBlock>().Update(block);
        }

        public void Delete(AvailabilityBlock block)
        {
            _context.Set<AvailabilityBlock>().Remove(block);
        }
    }
}
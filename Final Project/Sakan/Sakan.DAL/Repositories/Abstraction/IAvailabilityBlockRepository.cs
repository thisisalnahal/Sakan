using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IAvailabilityBlockRepository
    {
        Task<IEnumerable<AvailabilityBlock>> GetAllAsync();
        Task<AvailabilityBlock?> GetByIdAsync(int id);
        Task<IEnumerable<AvailabilityBlock>> GetByRentalUnitIdAsync(int rentalUnitId);
        Task AddAsync(AvailabilityBlock block);
        void Update(AvailabilityBlock block);
        void Delete(AvailabilityBlock block);
    }
}
using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    public interface IListingRepository
    {
        Task<IEnumerable<Listing>> GetAllAsync();
        Task<Listing?> GetByIdAsync(int id);
        Task AddAsync(Listing listing);
        void Update(Listing listing);
        void Delete(Listing listing);
    }
}
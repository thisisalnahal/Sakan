using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IDisputeResolutionRepository
    {
        (bool, string?) Create(DisputeResolution disputeResolution);
        (bool, string?) Edit(DisputeResolution newDisputeResolution, int oldDisputeResolutionId);
        (bool, string?, List<DisputeResolution>?) GetAll();
        (bool, string?, DisputeResolution?) GetById(int DisputeResolutionId);
        (bool, string?) Delete(int DisputeResolutionId);
    }
}

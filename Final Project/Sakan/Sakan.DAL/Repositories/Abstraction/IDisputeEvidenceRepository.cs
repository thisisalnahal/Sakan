using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IDisputeEvidenceRepository
    {
        (bool, string?) Create(DisputeEvidence disputeEvidence);
        (bool, string?) Edit(DisputeEvidence newDisputeEvidence, int oldDisputeEvidenceId);
        (bool, string?, List<DisputeEvidence>?) GetAll();
        (bool, string?, DisputeEvidence?) GetById(int DisputeEvidenceId);
        (bool, string?) Delete(int DisputeEvidenceId);
    }
}

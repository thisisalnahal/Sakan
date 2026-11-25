using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IDisputeRepository
    {
        (bool, string?) Create(Dispute dispute);
        (bool, string?) Edit(Dispute newDispute, int oldDisputeId);
        (bool, string?, List<Dispute>?) GetAll();
        (bool, string?, Dispute?) GetById(int DisputeId);
        (bool, string?) Delete(int DisputeId);
    }
}

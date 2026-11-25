using Sakan.DAL.Entities;
using Sakan.DAL.Enums.IdentityVerification;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IIdentityVerificationRepo
    {
        (bool Success, string Message) Add(IdentityVerification verification);

        (bool Success, string Message, IdentityVerification? Data) GetById(int id);

        (bool Success, string Message, IdentityVerification? Data) GetLatestByUser(string userId);

        (bool Success, string Message, List<IdentityVerification> Data, int TotalCount) GetPaged(
            Status? status, int page, int pageSize);

        (bool Success, string Message) Update(IdentityVerification verification);
    }
}

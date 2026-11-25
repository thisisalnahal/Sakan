using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IApplicationUserRepo
    {
        (bool Success, string Message, ApplicationUser? Data) GetById(string id);
        (bool Success, string Message, ApplicationUser? Data) GetByEmail(string email);

        (bool Success, string Message, List<ApplicationUser> Data, int TotalCount) Search(
            string? term, bool? isActive, int page, int pageSize);

        (bool Success, string Message) Add(ApplicationUser user);

        (bool Success, string Message) Update(ApplicationUser user);
    }
}

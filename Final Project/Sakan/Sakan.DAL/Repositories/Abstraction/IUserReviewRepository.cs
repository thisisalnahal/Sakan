using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IUserReviewRepository
    {
        (bool, string?) Create(UserReview userReview);
        (bool, string?) Edit(UserReview newUserReview, int oldUserReviewId);
        (bool, string?, List<UserReview>?) GetAll();
        (bool, string?, UserReview?) GetById(int UserReviewId);
        (bool, string?) Delete(int UserReviewId);
    }
}

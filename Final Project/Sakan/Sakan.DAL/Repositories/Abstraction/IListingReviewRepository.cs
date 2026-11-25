using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IListingReviewRepository
    {
        (bool, string?) Create(ListingReview listingReview);
        (bool, string?) Edit(ListingReview newListingReview, int oldListingReviewId);
        (bool, string?, List<ListingReview>?) GetAll();
        (bool, string?, ListingReview?) GetById(int ListingReviewId);
        (bool, string?) Delete(int ListingReviewId);
    }
}

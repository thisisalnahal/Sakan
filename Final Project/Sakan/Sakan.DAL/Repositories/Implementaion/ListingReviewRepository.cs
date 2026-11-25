using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class ListingReviewRepository : IListingReviewRepository
    {
        private readonly ApplicationDbContext Db;

        public ListingReviewRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(ListingReview listingReview)
        {
            try
            {
                Db.ListingReviews.Add(listingReview);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(ListingReview newListingReview, int oldListingReviewId)
        {
            try
            {
                var oldListingReview = Db.ListingReviews.FirstOrDefault(a => a.Id == oldListingReviewId);
                if (oldListingReview == null)
                    return (true, "There is no Data :(");

                oldListingReview.BookingId = newListingReview.BookingId;
                oldListingReview.Rating = newListingReview.Rating;
                oldListingReview.Comment = newListingReview.Comment;
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<ListingReview>?) GetAll()
        {
            try
            {
                var result = Db.ListingReviews.ToList();
                if (result.Count > 0)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, ListingReview?) GetById(int ListingReviewId)
        {
            try
            {
                var result = Db.ListingReviews.FirstOrDefault(a => a.Id == ListingReviewId);
                if (result != null)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int ListingReviewId)
        {
            try
            {
                var result = Db.ListingReviews.FirstOrDefault(a => a.Id == ListingReviewId);
                if (result == null)
                    return (true, "There is no Data :(");

                Db.ListingReviews.Remove(result);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }
    }
}

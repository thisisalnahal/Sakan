using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class UserReviewRepository : IUserReviewRepository
    {
        private readonly ApplicationDbContext Db;

        public UserReviewRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(UserReview userReview)
        {
            try
            {
                Db.UserReviews.Add(userReview);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(UserReview newUserReview, int oldUserReviewId)
        {
            try
            {
                var oldUserReview = Db.UserReviews.FirstOrDefault(a => a.Id == oldUserReviewId);
                if (oldUserReview == null)
                    return (true, "There is no Data :(");

                oldUserReview.BookingId = newUserReview.BookingId;
                oldUserReview.ReviewerId = newUserReview.ReviewerId;
                oldUserReview.RevieweeId = newUserReview.RevieweeId;
                oldUserReview.Rating = newUserReview.Rating;
                oldUserReview.Comment = newUserReview.Comment;
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<UserReview>?) GetAll()
        {
            try
            {
                var result = Db.UserReviews.ToList();
                if (result.Count > 0)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, UserReview?) GetById(int UserReviewId)
        {
            try
            {
                var result = Db.UserReviews.FirstOrDefault(a => a.Id == UserReviewId);
                if (result != null)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int UserReviewId)
        {
            try
            {
                var result = Db.UserReviews.FirstOrDefault(a => a.Id == UserReviewId);
                if (result == null)
                    return (true, "There is no Data :(");

                Db.UserReviews.Remove(result);
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

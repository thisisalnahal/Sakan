using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class DisputeRepository : IDisputeRepository
    {
        private readonly ApplicationDbContext Db;

        public DisputeRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(Dispute dispute)
        {
            try
            {
                Db.Disputes.Add(dispute);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(Dispute newDispute, int oldDisputeId)
        {
            try
            {
                var oldDispute = Db.Disputes.FirstOrDefault(a => a.Id == oldDisputeId);
                if (oldDispute == null)
                    return (true, "There is no Data :(");

                oldDispute.BookingId = newDispute.BookingId;
                oldDispute.DisputeType = newDispute.DisputeType;
                oldDispute.ReportedById = newDispute.ReportedById;
                oldDispute.Category = newDispute.Category;
                oldDispute.Description = newDispute.Description;
                oldDispute.ClaimedAmount = newDispute.ClaimedAmount;
                oldDispute.CounterpartyResponse = newDispute.CounterpartyResponse;
                oldDispute.RespondedAt = newDispute.RespondedAt;
                oldDispute.Status = newDispute.Status;
                oldDispute.ReviewDueAt = newDispute.ReviewDueAt;
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<Dispute>?) GetAll()
        {
            try
            {
                var result = Db.Disputes.ToList();
                if (result.Count > 0)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, Dispute?) GetById(int DisputeId)
        {
            try
            {
                var result = Db.Disputes.FirstOrDefault(a => a.Id == DisputeId);
                if (result != null)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int DisputeId)
        {
            try
            {
                var result = Db.Disputes.FirstOrDefault(a => a.Id == DisputeId);
                if (result == null)
                    return (true, "There is no Data :(");

                Db.Disputes.Remove(result);
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

using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class DisputeResolutionRepository : IDisputeResolutionRepository
    {
        private readonly ApplicationDbContext Db;

        public DisputeResolutionRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(DisputeResolution disputeResolution)
        {
            try
            {
                Db.DisputeResolutions.Add(disputeResolution);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(DisputeResolution newDisputeResolution, int oldDisputeResolutionId)
        {
            try
            {
                var oldDisputeResolution = Db.DisputeResolutions.FirstOrDefault(a => a.Id == oldDisputeResolutionId);
                if (oldDisputeResolution == null)
                    return (true, "There is no Data :(");

                oldDisputeResolution.DisputeId = newDisputeResolution.DisputeId;
                oldDisputeResolution.AdminId = newDisputeResolution.AdminId;
                oldDisputeResolution.Decision = newDisputeResolution.Decision;
                oldDisputeResolution.GuestRefundAmount = newDisputeResolution.GuestRefundAmount;
                oldDisputeResolution.HostReleaseAmount = newDisputeResolution.HostReleaseAmount;
                oldDisputeResolution.DepositCapturedAmount = newDisputeResolution.DepositCapturedAmount;
                oldDisputeResolution.AdminNotes = newDisputeResolution.AdminNotes;
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<DisputeResolution>?) GetAll()
        {
            try
            {
                var result = Db.DisputeResolutions.ToList();
                if (result.Count > 0)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, DisputeResolution?) GetById(int DisputeResolutionId)
        {
            try
            {
                var result = Db.DisputeResolutions.FirstOrDefault(a => a.Id == DisputeResolutionId);
                if (result != null)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int DisputeResolutionId)
        {
            try
            {
                var result = Db.DisputeResolutions.FirstOrDefault(a => a.Id == DisputeResolutionId);
                if (result == null)
                    return (true, "There is no Data :(");

                Db.DisputeResolutions.Remove(result);
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

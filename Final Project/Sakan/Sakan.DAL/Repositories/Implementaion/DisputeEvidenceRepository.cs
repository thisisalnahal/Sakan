using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class DisputeEvidenceRepository : IDisputeEvidenceRepository
    {
        private readonly ApplicationDbContext Db;

        public DisputeEvidenceRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(DisputeEvidence disputeEvidence)
        {
            try
            {
                Db.DisputeEvidences.Add(disputeEvidence);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(DisputeEvidence newDisputeEvidence, int oldDisputeEvidenceId)
        {
            try
            {
                var oldDisputeEvidence = Db.DisputeEvidences.FirstOrDefault(a => a.Id == oldDisputeEvidenceId);
                if (oldDisputeEvidence == null)
                    return (true, "There is no Data :(");

                oldDisputeEvidence.DisputeId = newDisputeEvidence.DisputeId;
                oldDisputeEvidence.UploadedById = newDisputeEvidence.UploadedById;
                oldDisputeEvidence.StorageKey = newDisputeEvidence.StorageKey;
                oldDisputeEvidence.FileName = newDisputeEvidence.FileName;
                oldDisputeEvidence.ContentType = newDisputeEvidence.ContentType;
                oldDisputeEvidence.FileSizeBytes = newDisputeEvidence.FileSizeBytes;
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<DisputeEvidence>?) GetAll()
        {
            try
            {
                var result = Db.DisputeEvidences.ToList();
                if (result.Count > 0)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, DisputeEvidence?) GetById(int DisputeEvidenceId)
        {
            try
            {
                var result = Db.DisputeEvidences.FirstOrDefault(a => a.Id == DisputeEvidenceId);
                if (result != null)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int DisputeEvidenceId)
        {
            try
            {
                var result = Db.DisputeEvidences.FirstOrDefault(a => a.Id == DisputeEvidenceId);
                if (result == null)
                    return (true, "There is no Data :(");

                Db.DisputeEvidences.Remove(result);
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

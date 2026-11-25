using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext Db;

        public ConversationRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(Conversation conversation)
        {
            try
            {
                Db.Conversations.Add(conversation);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(Conversation newConversation, int oldConversationId)
        {
            try
            {
                var oldConversation = Db.Conversations.FirstOrDefault(a => a.Id == oldConversationId);
                if (oldConversation == null)
                    return (true, "There is no Data :(");

                oldConversation.BookingId = newConversation.BookingId;
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<Conversation>?) GetAll()
        {
            try
            {
                var result = Db.Conversations.ToList();
                if (result.Count > 0)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, Conversation?) GetById(int ConversationId)
        {
            try
            {
                var result = Db.Conversations.FirstOrDefault(a => a.Id == ConversationId);
                if (result != null)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int ConversationId)
        {
            try
            {
                var result = Db.Conversations.FirstOrDefault(a => a.Id == ConversationId);
                if (result == null)
                    return (true, "There is no Data :(");

                Db.Conversations.Remove(result);
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

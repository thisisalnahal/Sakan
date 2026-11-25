using Sakan.DAL.DataBase;
using Sakan.DAL.Entities;
using Sakan.DAL.Repositories.Abstraction;

namespace Sakan.DAL.Repositories.Implementaion
{
    internal class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext Db;

        public MessageRepository(ApplicationDbContext db)
        {
            Db = db;
        }

        public (bool, string?) Create(Message message)
        {
            try
            {
                Db.Messages.Add(message);
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?) Edit(Message newMessage, int oldMessageId)
        {
            try
            {
                var oldMessage = Db.Messages.FirstOrDefault(a => a.Id == oldMessageId);
                if (oldMessage == null)
                    return (true, "There is no Data :(");

                oldMessage.ConversationId = newMessage.ConversationId;
                oldMessage.SenderId = newMessage.SenderId;
                oldMessage.Content = newMessage.Content;
                oldMessage.IsRead = newMessage.IsRead;
                Db.SaveChanges();
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string?, List<Message>?) GetAll()
        {
            try
            {
                var result = Db.Messages.ToList();
                if (result.Count > 0)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?, Message?) GetById(int MessageId)
        {
            try
            {
                var result = Db.Messages.FirstOrDefault(a => a.Id == MessageId);
                if (result != null)
                    return (false, null, result);
                return (true, "There is no Data :(", null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) Delete(int MessageId)
        {
            try
            {
                var result = Db.Messages.FirstOrDefault(a => a.Id == MessageId);
                if (result == null)
                    return (true, "There is no Data :(");

                Db.Messages.Remove(result);
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

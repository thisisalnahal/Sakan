using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IMessageRepository
    {
        (bool, string?) Create(Message message);
        (bool, string?) Edit(Message newMessage, int oldMessageId);
        (bool, string?, List<Message>?) GetAll();
        (bool, string?, Message?) GetById(int MessageId);
        (bool, string?) Delete(int MessageId);
    }
}

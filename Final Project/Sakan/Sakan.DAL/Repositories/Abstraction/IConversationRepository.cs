using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IConversationRepository
    {
        (bool, string?) Create(Conversation conversation);
        (bool, string?) Edit(Conversation newConversation, int oldConversationId);
        (bool, string?, List<Conversation>?) GetAll();
        (bool, string?, Conversation?) GetById(int ConversationId);
        (bool, string?) Delete(int ConversationId);
    }
}

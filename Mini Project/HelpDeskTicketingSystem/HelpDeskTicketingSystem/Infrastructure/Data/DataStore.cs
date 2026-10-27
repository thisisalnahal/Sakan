using HelpDeskTicketingSystem.Domain.Entities;

namespace HelpDeskTicketingSystem.Infrastructure.Data
{
    internal class DataStore
    {
        private int _nextUserId = 1;
        private int _nextTicketId = 1;
        private int _nextCommentId = 1;

        public List<User> Users { get; } = new List<User>();
        public List<Ticket> Tickets { get; } = new List<Ticket>();

        public int NextUserId() => _nextUserId++;
        public int NextTicketId() => _nextTicketId++;
        public int NextCommentId() => _nextCommentId++;
    }
}
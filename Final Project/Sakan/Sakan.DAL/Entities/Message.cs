namespace Sakan.DAL.Entities
{
    internal class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public string SenderId { get; set; } 
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }

        // Navigation Properties
        public Conversation Conversation { get; set; }
        public ApplicationUser Sender { get; set; } 
    }
}

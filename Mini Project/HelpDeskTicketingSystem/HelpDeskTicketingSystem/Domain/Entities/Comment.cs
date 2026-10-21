namespace HelpDeskTicketingSystem.Domain.Entities
{
    internal class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;

        public int AuthorId { get; set; }
        public User Author { get; set; } = null!;
    }
}
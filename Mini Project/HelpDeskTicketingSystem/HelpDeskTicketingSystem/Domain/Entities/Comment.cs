namespace HelpDeskTicketingSystem.Domain.Entities
{
    internal class Comment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
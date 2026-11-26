namespace Sakan.DAL.Entities
{
    public class Conversation
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DateTime CreatedAt { get; set; } 

        // Navigation Properties
        public Booking Booking { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}

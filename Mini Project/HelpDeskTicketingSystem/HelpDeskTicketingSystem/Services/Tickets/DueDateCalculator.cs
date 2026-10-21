using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Interfaces;

namespace HelpDeskTicketingSystem.Services
{
    internal class DueDateCalculator : IDueDateCalculator
    {
        public DateTime Calculate(TicketPriority priority, DateTime from)
        {
            int hours = priority switch
            {
                TicketPriority.Urgent => 4,
                TicketPriority.High => 24,
                TicketPriority.Medium => 48,
                TicketPriority.Low => 72,
                _ => throw new ArgumentOutOfRangeException(nameof(priority))
            };

            return from.AddHours(hours);
        }
    }
}
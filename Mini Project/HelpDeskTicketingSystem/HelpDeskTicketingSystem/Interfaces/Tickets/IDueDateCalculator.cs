using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Interfaces.Tickets
{
    internal interface IDueDateCalculator
    {
        DateTime Calculate(TicketPriority priority, DateTime from);
    }
}
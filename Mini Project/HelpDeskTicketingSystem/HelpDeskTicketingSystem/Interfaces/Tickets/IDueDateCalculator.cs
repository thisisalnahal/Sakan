using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface IDueDateCalculator
    {
        DateTime Calculate(TicketPriority priority, DateTime from);
    }
}
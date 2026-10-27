using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Interfaces.Tickets
{
    internal interface IAssignmentService
    {
        Agent? GetLeastBusyAgent(Department department);
    }
}
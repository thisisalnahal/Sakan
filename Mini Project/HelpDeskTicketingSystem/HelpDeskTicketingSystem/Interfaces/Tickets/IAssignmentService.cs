using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;

namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface IAssignmentService
    {
        Agent? GetLeastBusyAgent(Department department);
    }
}
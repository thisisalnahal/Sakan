using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Infrastructure.Data;
using HelpDeskTicketingSystem.Interfaces.Tickets;

namespace HelpDeskTicketingSystem.Services.Tickets
{
    internal class AssignmentService : IAssignmentService
    {
        public Agent? GetLeastBusyAgent(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
namespace HelpDeskTicketingSystem.Interfaces.Tickets
{
    internal interface IAutoCloseService
    {
        int CloseExpiredResolvedTickets();
    }
}
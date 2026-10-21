namespace HelpDeskTicketingSystem.Interfaces
{
    internal interface IAutoCloseService
    {
        int CloseExpiredResolvedTickets();
    }
}
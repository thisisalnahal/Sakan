namespace HelpDeskTicketingSystem.Presentation.UI
{
    internal interface IConsoleUI
    {
        void ClearScreen();
        void Pause();

        void PrintHeader(string title);
        void PrintDivider();
        void PrintSuccess(string message);
        void PrintError(string message);
        void PrintWarning(string message);
        void PrintInfo(string message);
        void PrintLine(string message);
        void Print(string message);

        int PrintMenu(string title, string[] options);
        void PrintTable(string[] headers, List<string[]> rows, ConsoleColor[]? rowColors = null);

        string ReadName(string prompt);
        string ReadText(string prompt);
        string ReadEmail(string prompt);
        string ReadPassword(string prompt);
        int ReadInt(string prompt);
        bool Confirm(string prompt);
    }
}
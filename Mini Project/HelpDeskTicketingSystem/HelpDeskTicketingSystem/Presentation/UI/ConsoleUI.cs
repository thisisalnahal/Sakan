namespace HelpDeskTicketingSystem.Presentation
{
    internal class ConsoleUI : IConsoleUI
    {
        private const int Width = 100;

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void Pause()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.ResetColor();
        }

        public void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', Width));
            int padding = (Width - title.Length) / 2;
            Console.WriteLine(new string(' ', Math.Max(padding, 0)) + title);
            Console.WriteLine(new string('=', Width));
            Console.ResetColor();
        }

        public void PrintDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('-', Width));
            Console.ResetColor();
        }

        public void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  [OK] " + message);
            Console.ResetColor();
        }

        public void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [ERROR] " + message);
            Console.ResetColor();
        }

        public void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  [WARNING] " + message);
            Console.ResetColor();
        }

        public void PrintInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  " + message);
            Console.ResetColor();
        }

        public void PrintLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();
        }

        public int PrintMenu(string title, string[] options)
        {
            PrintHeader(title);

            for (int i = 0; i < options.Length; i++)
                PrintInfo($"{i + 1} - {options[i]}");

            while (true)
            {
                Print("Select an option: ");

                if (int.TryParse(ReadInput(), out int choice) && choice >= 1 && choice <= options.Length)
                    return choice;

                PrintError("Invalid Option. Try again!");
            }
        }

        public void PrintTable(string[] headers, List<string[]> rows, ConsoleColor[]? rowColors = null)
        {
            int[] colWidths = new int[headers.Length];

            for (int i = 0; i < headers.Length; i++)
                colWidths[i] = headers[i].Length;

            foreach (string[] row in rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i].Length > colWidths[i])
                        colWidths[i] = row[i].Length;
                }
            }

            PrintRow(headers, colWidths, ConsoleColor.Cyan);
            Console.WriteLine(new string('-', colWidths.Sum() + (colWidths.Length - 1) * 2));

            for (int i = 0; i < rows.Count; i++)
            {
                ConsoleColor color = ConsoleColor.White;

                if (rowColors != null && i < rowColors.Length)
                    color = rowColors[i];

                PrintRow(rows[i], colWidths, color);
            }
        }

        public string ReadName(string prompt)
        {
            while (true)
            {
                Print(prompt);
                string input = ReadInput();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                PrintError("Name cannot be empty.");
            }
        }

        public string ReadText(string prompt)
        {
            while (true)
            {
                Print(prompt);
                string input = ReadInput();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                PrintError("This field cannot be empty.");
            }
        }

        public string ReadEmail(string prompt)
        {
            while (true)
            {
                Print(prompt);
                string input = ReadInput();

                if (input.Contains('@') && !input.Contains(' '))
                    return input;

                PrintError("Enter a valid email that contains @.");
            }
        }

        public string ReadPassword(string prompt)
        {
            while (true)
            {
                Print(prompt);
                string input = ReadInput();

                if (input.Length >= 8)
                    return input;

                PrintError("Password must be at least 8 characters.");
            }
        }

        public int ReadInt(string prompt)
        {
            while (true)
            {
                Print(prompt);

                if (int.TryParse(ReadInput(), out int value))
                    return value;

                PrintError("Please enter a valid number.");
            }
        }

        public bool Confirm(string prompt)
        {
            while (true)
            {
                Print(prompt + " (y/n): ");
                string input = ReadInput().ToLower();

                if (input == "y") return true;
                if (input == "n") return false;

                PrintError("Please enter y or n.");
            }
        }

        private static string ReadInput()
        {
            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        private static void PrintRow(string[] cells, int[] colWidths, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            for (int i = 0; i < cells.Length; i++)
                Console.Write(cells[i].PadRight(colWidths[i] + 2));

            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
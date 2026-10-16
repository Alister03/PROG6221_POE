using System;
using System.Threading;

namespace Prog6221_A1
{
    public static class ConsoleUI
    {
        public static void PrintColoured(string message, ConsoleColor colour)
        {
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = colour;
            Console.WriteLine(message);
            Console.ForegroundColor = previous;
        }

        public static void PrintPrompt(string message, ConsoleColor colour)
        {
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = colour;
            Console.Write(message); // no new line, so the user types on the same line
            Console.ForegroundColor = previous;
        }

        //  typing effect for a conversational feel and interaction
        public static void TypeOut(string message, ConsoleColor colour, int delayMs = 20)
        {
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = colour;

            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();

            Console.ForegroundColor = previous;
        }

        public static void DrawDivider()
        {
            PrintColoured(new string('=', 60), ConsoleColor.DarkCyan);
        }

        public static void PrintHeader(string title)
        {
            DrawDivider();
            PrintColoured("   " + title, ConsoleColor.Cyan);
            DrawDivider();
        }
    }
}
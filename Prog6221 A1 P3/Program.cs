using Prog6221_A1;
using System;

namespace Prog6221_A1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";

            // Requirement 2: ASCII logo on launch
            ConsoleUI.PrintColoured(AsciiArt.Logo, ConsoleColor.Cyan);

            // Requirement 1: voice greeting
            AudioPlayer.PlayGreeting("greeting.wav");

            // Requirements 3-5: the conversation loop
            Chatbot bot = new Chatbot();
            bot.Start();
        }
    }
}
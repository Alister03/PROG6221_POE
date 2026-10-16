using Prog6221_A1;
using System;

namespace Prog6221_A1
{
    public class Chatbot
    {
        private readonly ResponseHandler responder = new ResponseHandler();
        private readonly ChatbotUser user = new ChatbotUser();

        public void Start()
        {
            WelcomeUser();

            bool chatting = true;
            while (chatting)
            {
                ConsoleUI.PrintPrompt("\nYou: ", ConsoleColor.Green);
                string rawInput = Console.ReadLine();

                // Requirement 5: input validation
                if (!InputValidator.IsValid(rawInput, out string cleanedInput))
                {
                    ConsoleUI.TypeOut("Bot: I didn't quite catch that. Please type something for me to answer.", ConsoleColor.Red);
                    continue;
                }

                if (InputValidator.WantsToExit(cleanedInput))
                {
                    ConsoleUI.TypeOut($"Bot: Goodbye, {user.UserName}! You asked me {user.QuestionsAsked} question(s) today. Stay safe online!", ConsoleColor.Cyan);
                    chatting = false;
                    continue;
                }

                // Requirement 4: basic response system
                string reply = responder.GetResponse(cleanedInput, user.UserName);
                user.RecordQuestion();

                ConsoleUI.TypeOut("Bot: " + reply, ConsoleColor.Cyan);
            }
        }

        // Requirement 3: text-based greeting with personalisation
        private void WelcomeUser()
        {
            ConsoleUI.PrintHeader("CYBERSECURITY AWARENESS BOT");

            ConsoleUI.TypeOut("Bot: Hello! Before we start, what's your name?", ConsoleColor.Cyan);

            while (true)
            {
                ConsoleUI.PrintPrompt("You: ", ConsoleColor.Green);
                string rawName = Console.ReadLine();

                if (InputValidator.IsValid(rawName, out string name))
                {
                    user.UserName = name;
                    break;
                }

                ConsoleUI.TypeOut("Bot: I didn't quite get that. Please tell me your name.", ConsoleColor.Red);
            }

            ConsoleUI.TypeOut($"Bot: Great to meet you, {user.UserName}! Ask me about password safety, phishing, or safe browsing. Type 'exit' when you're done.", ConsoleColor.Cyan);
        }
    }
}
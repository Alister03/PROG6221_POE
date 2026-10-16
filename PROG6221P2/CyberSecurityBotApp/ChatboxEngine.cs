using System;
using System.Linq;

namespace CyberSecurityBotApp
{
    public class ChatbotEngine
    {
        // Delegates in action: the engine raises events, the GUI listens.
        public event BotMessageHandler? OnBotMessage;
        public event Action? OnExit;   // Action is a built-in delegate type

        private readonly ChatbotUser user = new();
        private readonly ResponseHandler responder = new();
        private bool awaitingName = true;
        private bool finished = false;

        private void Say(string message) => OnBotMessage?.Invoke(message);

        public void Start()
        {
            Say("Welcome to the Cybersecurity Awareness Bot!");
            Say("Before we start, what is your name?");
        }

        public void HandleUserInput(string rawInput)
        {
            if (finished) return;

            // Error handling: empty/meaningless input 
            if (!InputValidator.IsValid(rawInput, out string input))
            {
                Say("Please type a message so I can help you.");
                return;
            }

            if (InputValidator.WantsToExit(input))
            {
                finished = true;
                string who = user.UserName == null ? "" : $", {user.UserName}";
                Say($"Goodbye{who}! You asked me {user.QuestionsAsked} question(s) today. Stay safe online!");
                OnExit?.Invoke();
                return;
            }

            // Conversation flow: capture and remember the name first
            if (awaitingName)
            {
                if (input.Length < 2 || input.Any(char.IsDigit))
                {
                    Say("That doesn't look like a name. Please enter your name (letters only).");
                    return;
                }
                user.UserName = char.ToUpper(input[0]) + input.Substring(1);
                awaitingName = false;
                Say($"Great to meet you, {user.UserName}! Ask me about password safety, phishing, scams, " +
                    "privacy or safe browsing. Type 'exit' whenever you're done.");
                return;
            }

            user.RecordQuestion();
            Say(responder.GetResponse(input, user));
        }
    }
}
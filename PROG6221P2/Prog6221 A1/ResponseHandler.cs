using System.Collections.Generic;

namespace Prog6221_A1
{
    public class ResponseHandler
    {
        // Requirement 4: predefined responses for basic questions and topics
        private readonly Dictionary<string, string> responses = new Dictionary<string, string>
        {
            { "how are you", "I'm running perfectly, {name}! All systems are secure." },
            { "purpose", "My purpose, {name}, is to help you understand cybersecurity and stay safe online." },
            { "what can i ask", "You can ask me about password safety, phishing, and safe browsing." },
            { "password", "Passwords should be long (12+ characters) and mix letters, numbers and symbols. Never reuse them across accounts — consider a password manager." },
            { "phishing", "Phishing emails pretend to be trusted companies to steal your details, {name}. Always check the sender's address and never click suspicious links." },
            { "scam", "A good rule of thumb: if it sounds too good to be true, it probably is. Verify requests through official channels before responding." },
            { "safe browsing", "For safe browsing, look for HTTPS in the address bar, avoid sensitive tasks on public Wi-Fi, and keep your browser up to date." },
            { "wifi", "Public Wi-Fi is convenient but risky. Avoid logging into banking or email on open networks, or use a trusted VPN." },
            { "2fa", "Two-factor authentication adds a second lock to your accounts. Turn it on wherever possible, {name}." },
            { "hello", "Hello, {name}! What would you like to learn about today?" },
            { "hi", "Hi there, {name}! Ask me anything about staying safe online." }
        };

        public string GetResponse(string input, string userName)
        {
            // String manipulation: normalise the user's question
            string normalised = input.ToLower();

            foreach (KeyValuePair<string, string> entry in responses)
            {
                if (normalised.Contains(entry.Key))
                {
                    // String manipulation: swap the {name} token for the real name
                    return entry.Value.Replace("{name}", userName);
                }
            }

            // Requirement 5: default response for anything we don't recognise
            return $"I didn't quite understand that, {userName}. Could you rephrase? " +
                   "You can ask me about passwords, phishing, or safe browsing.";
        }
    }
}
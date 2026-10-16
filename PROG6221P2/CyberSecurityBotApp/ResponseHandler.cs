using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityBotApp
{
    public class ResponseHandler
    {
        private readonly Random rng = new();

        
        // Each keyword maps to a List<string> pool so the bot can pick a
        // RANDOM tip every time.
        private readonly Dictionary<string, List<string>> topicResponses;
        private readonly Dictionary<string, List<string>> generalResponses;

        // keyword -> friendly topic name, used for memory & recall 
        private static readonly Dictionary<string, string> friendlyNames = new()
        {
            ["password"] = "password safety",
            ["2fa"] = "two-factor authentication",
            ["phishing"] = "phishing",
            ["scam"] = "scams",
            ["privacy"] = "privacy",
            ["browsing"] = "safe browsing",
            ["wifi"] = "public Wi-Fi safety"
        };

        private static readonly string[] followUpTriggers =
            { "another", "more", "explain", "example", "why", "elaborate" };

        private static readonly string[] memoryTriggers =
            { "interested in", "want to learn about", "i like", "i care about", "teach me about" };

        private static readonly string[] followUpStarters =
            { "Sure — here's another tip:", "Of course! Did you know:", "Happy to share more:", "Here's another one:" };

        private string? lastTopic;   // remembers the current topic for follow-ups

        public ResponseHandler()
        {
            // Each pool is a List<string> — add more strings to add variety.
            List<string> passwordTips = new()
            {
                "Use long passwords — at least 12 characters mixing letters, numbers and symbols.",
                "Never reuse passwords across accounts; one data leak would expose everything.",
                "A password manager creates and stores strong passwords so you only remember one.",
                "Avoid personal details like birthdays or pet names — attackers check social media first."
            };

            List<string> twoFaTips = new()
            {
                "Two-factor authentication (2FA) adds a second lock to your accounts — turn it on everywhere.",
                "Prefer an authenticator app over SMS for 2FA; SIM-swap attacks can intercept text codes.",
                "Save your 2FA backup codes somewhere safe, like a printed copy in a locked drawer."
            };

            List<string> phishingTips = new()
            {
                "Be cautious of emails asking for personal information — scammers disguise themselves as trusted organisations.",
                "Check the sender's full address carefully; 'support@paypa1.com' is not PayPal.",
                "Hover over links before clicking to preview the real destination.",
                "Urgency is a red flag: 'act now or your account closes' is a classic phishing tactic."
            };

            List<string> scamTips = new()
            {
                "If an offer sounds too good to be true, it probably is.",
                "Never send money or gift cards to someone you haven't verified — gift cards are a scammer's favourite.",
                "Verify unexpected requests by calling the organisation on their official number, never the one in the message.",
                "Scams rely on secrecy — talk to someone you trust before paying anything."
            };

            List<string> privacyTips = new()
            {
                "Review the privacy settings on your social media — share less, stay safer.",
                "Think before you post: locations, ID numbers and travel plans are gold for attackers.",
                "Use an alias email address for newsletters so you can trace who leaked your details.",
                "Search for your own name occasionally to see what personal info is publicly visible."
            };

            List<string> browsingTips = new()
            {
                "Look for HTTPS in the address bar before entering any personal details.",
                "Avoid banking or shopping on public Wi-Fi, or use a trusted VPN if you must.",
                "Keep your browser and extensions updated — old versions have known vulnerabilities.",
                "Ignore pop-ups claiming your PC is infected; they're scareware."
            };

            List<string> wifiTips = new()
            {
                "Public Wi-Fi is convenient but risky — assume anyone nearby can eavesdrop.",
                "Turn off automatic Wi-Fi connection so your device doesn't join fake hotspots.",
                "Use mobile data or a VPN for sensitive tasks on open networks."
            };

            topicResponses = new Dictionary<string, List<string>>
            {
                ["password"] = passwordTips,
                ["2fa"] = twoFaTips,
                ["phishing"] = phishingTips,
                ["scam"] = scamTips,
                ["privacy"] = privacyTips,
                ["browsing"] = browsingTips,
                ["wifi"] = wifiTips
            };

            generalResponses = new Dictionary<string, List<string>>
            {
                ["how are you"] = new List<string>
                {
                    "I'm running perfectly — all systems secure!",
                    "Feeling great and ready to help you stay safe online."
                },
                ["purpose"] = new List<string>
                {
                    "My purpose is to help you understand cybersecurity and stay safe online.",
                    "I exist to teach practical cybersecurity habits in plain language."
                },
                ["what can i ask"] = new List<string>
                {
                    "You can ask me about password safety, 2FA, phishing, scams, privacy, safe browsing or public Wi-Fi."
                },
                ["hello"] = new List<string> { "Hello, {name}! What would you like to learn about today?" },
                ["hi"] = new List<string> { "Hi there, {name}! Ask me anything about staying safe online." }
            };
        }

        public string GetResponse(string input, ChatbotUser user)
        {
            string text = input.ToLower();

            // 1) Sentiment detection — empathise, but STILL answer in the same
            //    message so the user doesn't have to ask again (requirement 6).
            string empathy = SentimentDetector.BuildPrefix(text);

            // 2) Memory capture: "I'm interested in privacy" (requirement 5)
            foreach (string trigger in memoryTriggers)
            {
                if (!text.Contains(trigger)) continue;

                foreach (KeyValuePair<string, string> topic in friendlyNames)
                {
                    if (!text.Contains(topic.Key)) continue;

                    user.FavouriteTopic = topic.Value;   // remember it!
                    lastTopic = topic.Key;               // follow-ups now work too
                    return empathy +
                        $"Great, {user.UserName}! I'll remember that you're interested in {topic.Value}. " +
                        "It's a crucial part of staying safe online. Ask me anything about it — or say 'tell me more'.";
                }
            }

            // 3) Follow-ups: keep the current topic going 
            if (followUpTriggers.Any(t => text.Contains(t)))
            {
                if (lastTopic == null)
                    return empathy + $"Tell me a topic first — passwords, phishing, scams, privacy or safe browsing — " +
                           $"and I'll gladly explain more, {user.UserName}!";

                string transition = followUpStarters[rng.Next(followUpStarters.Length)];
                return empathy + transition + " " + PickTip(lastTopic, user);
            }

            // 4) General small talk
            foreach (KeyValuePair<string, List<string>> entry in generalResponses)
            {
                if (text.Contains(entry.Key))
                    return empathy + entry.Value[rng.Next(entry.Value.Count)].Replace("{name}", user.UserName ?? "friend");
            }

            // 5) Keyword recognition.
            //    Longest keys first so longer phrases aren't shadowed by short ones.
            foreach (string keyword in topicResponses.Keys.OrderByDescending(k => k.Length))
            {
                if (text.Contains(keyword))
                {
                    lastTopic = keyword;
                    return empathy + PickTip(keyword, user);
                }
            }

            // 6) Default fallback for unknown input 
            string recall = user.FavouriteTopic == null
                ? ""
                : $" Since you're interested in {user.FavouriteTopic}, you could ask me more about that!";
            return empathy + $"I'm not sure I understand, {user.UserName}. Can you try rephrasing? " +
                   "You can ask me about passwords, phishing, scams, privacy or safe browsing." + recall;
        }

        // Randomly selects a tip, and personalises it if it's the user's
        // favourite topic (memory recall)
        private string PickTip(string topic, ChatbotUser user)
        {
            List<string> pool = topicResponses[topic];
            string tip = pool[rng.Next(pool.Count)];

            if (user.FavouriteTopic == friendlyNames[topic])
                return $"As someone interested in {user.FavouriteTopic}, {char.ToLower(tip[0])}{tip.Substring(1)}";

            return tip;
        }
    }
}
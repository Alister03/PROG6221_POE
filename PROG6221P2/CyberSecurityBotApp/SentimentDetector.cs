using System.Collections.Generic;

namespace CyberSecurityBotApp
{
    public static class SentimentDetector
    {
        // Sentiment word -> empathy
        private static readonly Dictionary<string, string> empathyLines = new()
        {
            ["worried"] = "It's completely understandable to feel that way — scammers can be very convincing. ",
            ["anxious"] = "That's a really common feeling, and learning to protect yourself already puts you ahead. ",
            ["scared"] = "No need to face it alone — knowing the warning signs makes a real difference. ",
            ["nervous"] = "Take a breath — these threats are manageable once you know the basics. ",
            ["frustrated"] = "I hear you — cybersecurity can feel like a lot. Let's take it one step at a time. ",
            ["overwhelmed"] = "That's okay — nobody masters everything at once. Small habits make a big difference. ",
            ["confused"] = "That's perfectly fine — let me explain it more clearly. ",
            ["unsure"] = "That's completely normal — everyone starts somewhere. ",
            ["curious"] = "Love the curiosity — asking questions is the best security habit there is! ",
            ["excited"] = "That's great energy — let's put it to good use! "
        };

        // Returns an empathetic prefix, or "" if no sentiment was detected.
        public static string BuildPrefix(string input)
        {
            foreach (KeyValuePair<string, string> line in empathyLines)
            {
                if (input.Contains(line.Key))
                    return line.Value;
            }
            return "";
        }
    }
}
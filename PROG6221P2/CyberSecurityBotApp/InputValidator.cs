using System.Linq;

namespace CyberSecurityBotApp
{
    public static class InputValidator
    {
        // detect empty input.
        // Also cleans input.
        public static bool IsValid(string rawInput, out string cleaned)
        {
            cleaned = string.Empty;

            if (string.IsNullOrWhiteSpace(rawInput))
                return false;

            string trimmed = rawInput.Trim(); // removes extra spaces
            string noPunctuation = new string(trimmed.Where(c => !char.IsPunctuation(c)).ToArray());

            cleaned = noPunctuation;
            return cleaned.Length > 0;
        }

        public static bool WantsToExit(string input)
        {
            string lower = input.ToLower();
            return lower == "exit" || lower == "quit" || lower == "bye" || lower == "goodbye";
        }
    }
}
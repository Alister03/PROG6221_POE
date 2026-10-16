namespace CyberSecurityBotApp
{
    public class ChatbotUser
    {
        // Automatic properties + memory recall 
        public string? UserName { get; set; }
        public string? FavouriteTopic { get; set; }   // recalled mid-conversation
        public int QuestionsAsked { get; private set; }

        public void RecordQuestion() => QuestionsAsked++;
    }
}
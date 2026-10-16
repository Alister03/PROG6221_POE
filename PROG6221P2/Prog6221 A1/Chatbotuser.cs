namespace Prog6221_A1
{
    public class ChatbotUser
    {
        // Automatic properties: the compiler secretly creates backing
        
        public string UserName { get; set; }
        public int QuestionsAsked { get; private set; }

        public void RecordQuestion()
        {
            QuestionsAsked++;
        }
    }
}
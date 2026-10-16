# PROG6221_POE
part 2 completion
What was changed:
1. Architecture- the front-end of the console was replaced and not the brain of the bot. The old console loop (Chatbot) which made use of Console.ReadLine and WriteLine was retired. A new class called ChatbotEngine now holds the conversation logic and knows nothing about WPF. This is called separation of concerns- the same engine can be used to drive a web or mobile front-end, which is what Part 3 requires.
2. Delegates- a custom delegate type called BotMessageHandler(string message) was introduced for decoupling (this is the core learning outcome). The delegate defines the "shape" of a method that receives the bot messages. The engine exposes an event of this type called OnBotMessage and the GUI subscribes its AppendBotMessage method to it. The engine raises the messages and the GUI decides how they will be displayed. The two layers never reference each other directly.

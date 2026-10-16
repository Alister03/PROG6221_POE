namespace CyberSecurityBotApp
{
    // Delegate type: a method that receives a bot message and returns nothing.
    // The engine exposes EVENTS of this type, so any front-end (WPF today,
    // a web or mobile app later) can subscribe without the engine knowing about it.
    public delegate void BotMessageHandler(string message);
}
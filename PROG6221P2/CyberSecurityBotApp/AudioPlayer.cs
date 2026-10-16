using System.IO;
using System.Media;

namespace CyberSecurityBotApp
{
    public static class AudioPlayer
    {
        // Voice greeting (transferred from Part 1). Play() is non-blocking,
        // so the window opens instantly while the audio plays.
        public static void PlayGreeting(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    SoundPlayer player = new SoundPlayer(filePath);
                    player.Play();
                }
                // If the file is missing the app still starts 
            }
            catch
            {
                // Never let an audio problem crash the chatbot 
            }
        }
    }
}
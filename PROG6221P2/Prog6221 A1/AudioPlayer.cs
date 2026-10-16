using Prog6221_A1;
using System;
using System.IO;
using System.Media;

namespace Prog6221_A1
{
    public static class AudioPlayer
    {
        // play the WAV greeting when the app starts
        public static void PlayGreeting(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (SoundPlayer player = new SoundPlayer(filePath))
                    {
                        player.PlaySync(); // waits for the audio to finish before chat starts up
                    }
                }
                else
                {
                    ConsoleUI.PrintColoured("Voice greeting file not found — skipping audio.", ConsoleColor.Yellow);
                }
            }
            catch (Exception ex)
            {
                ConsoleUI.PrintColoured("Could not play the voice greeting: " + ex.Message, ConsoleColor.Yellow);
            }
        }
    }
}
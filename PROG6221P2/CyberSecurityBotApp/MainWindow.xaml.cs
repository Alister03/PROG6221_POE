using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberSecurityBotApp
{
    public partial class MainWindow : Window
    {
        private readonly ChatbotEngine engine = new();

        private static readonly Brush BotBrush = new SolidColorBrush(Color.FromRgb(0x4E, 0xC9, 0xB0));
        private static readonly Brush UserBrush = new SolidColorBrush(Color.FromRgb(0x9C, 0xDC, 0xFE));

        public MainWindow()
        {
            InitializeComponent();
            AsciiBlock.Text = AsciiArt.Logo;   // ASCII art in the GUI

            // Delegates in action: the GUI subscribes to the engine's events.
            engine.OnBotMessage += AppendBotMessage;
            engine.OnExit += EndConversation;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AudioPlayer.PlayGreeting("greeting.wav");   // voice greeting on startup
            engine.Start();
            InputBox.Focus();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e) => Send();

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Send();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
            => engine.HandleUserInput("exit");

        private void Send()
        {
            string text = InputBox.Text;
            if (string.IsNullOrWhiteSpace(text)) return;   // ignore empty input
            InputBox.Clear();

            AppendUserMessage(text);
            engine.HandleUserInput(text);
            InputBox.Focus();
        }

        private void AppendBotMessage(string message) => AddLine("Bot:", message, BotBrush);
        private void AppendUserMessage(string message) => AddLine("You:", message, UserBrush);

        private void AddLine(string prefix, string message, Brush colour)
        {
            TextBlock line = new TextBlock
            {
                Text = prefix + " " + message,
                Foreground = colour,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                Margin = new Thickness(2, 3, 2, 3)
            };
            ChatList.Items.Add(line);
            ChatList.ScrollIntoView(line);
        }

        private void EndConversation()
        {
            InputBox.IsEnabled = false;
            SendButton.IsEnabled = false;
        }
    }
}
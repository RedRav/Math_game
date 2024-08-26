using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Math_game
{
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            Timer.Interval = TimeSpan.FromSeconds(.1);
            Timer.Tick += Timer_Tick;
            SetUpGame();

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                Timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "👩🏾‍", "👩🏾‍",
                "🚗", "🚗",
                "🍔", "🍔",
                "🍌", "🍌",
                "🎅", "🎅",
                "🤬", "🤬",
                "💩", "💩",
                "🐵", "🐵",
            };
            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(0, animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                    if (textBlock.Visibility == Visibility.Visible)
                    {
                        textBlock.MouseDown += TextBlock_MouseDown;                                        //максим
                    }
                    textBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    textBlock.MouseDown += TimeTextBlock_MouseDown;
                }
            }
            Timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        private TextBlock _lastTextBlock;
        private bool _findingMatch;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if(_findingMatch == false)
            {
                _lastTextBlock = textBlock;
                textBlock.Visibility = Visibility.Hidden;
                _findingMatch = true;
            }
            else if(textBlock.Text == _lastTextBlock.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                _findingMatch = false;
            }
            else
            {
                _lastTextBlock.Visibility = Visibility.Visible;
                _findingMatch = false;
            }
        }

        
    }
}
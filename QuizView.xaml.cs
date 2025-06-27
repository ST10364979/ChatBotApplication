using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ChatBotApplication.Views
{
    public partial class QuizView : UserControl
    {
        private List<Question> Questions = new();
        private int currentQuestionIndex = 0;
        private int score = 0;

        public QuizView()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayCurrentQuestion();
        }

        private void LoadQuestions()
        {
            Questions = new List<Question>
            {
                new Question("What is phishing?",
                    new[] { "An email scam", "A type of virus", "A firewall breach", "A Wi-Fi signal boost" }, 0),

                new Question("Which is the safest password?",
                    new[] { "123456", "password", "MyDog2024!", "abcdef" }, 2),

                new Question("What should you do with suspicious links?",
                    new[] { "Click them immediately", "Ignore and report them", "Forward them to friends", "Save them for later" }, 1),

                new Question("What is 2FA?",
                    new[] { "Second Firewall Application", "Two-Factor Authentication", "Two-Fold Access", "Twice Filtered Algorithm" }, 1),

                new Question("A VPN helps to...",
                    new[] { "Make browsing faster", "Hide your IP and encrypt data", "Give free internet", "Boost Wi-Fi signal" }, 1),

                new Question("Strong passwords should include...",
                    new[] { "Only numbers", "Only letters", "A mix of characters", "Just your name" }, 2),

                new Question("Which is a sign of a secure website?",
                    new[] { "It loads fast", "It has ads", "It uses HTTPS", "It has popups" }, 2),

                new Question("Cyberbullying should be...",
                    new[] { "Encouraged", "Ignored", "Reported", "Laughed at" }, 2)
            };
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex >= Questions.Count)
            {
                ShowFinalFeedback();
                return;
            }

            Question current = Questions[currentQuestionIndex];
            QuestionText.Text = current.Text;
            OptionsPanel.Children.Clear();

            for (int i = 0; i < current.Options.Length; i++)
            {
                var radio = new RadioButton
                {
                    Content = current.Options[i],
                    Tag = i,
                    GroupName = "QuizOptions",
                    FontSize = 16,
                    Foreground = System.Windows.Media.Brushes.White,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                OptionsPanel.Children.Add(radio);
            }
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            var selectedOption = OptionsPanel.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
            if (selectedOption == null)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            int selectedIndex = (int)selectedOption.Tag;
            if (selectedIndex == Questions[currentQuestionIndex].CorrectOption)
                score++;

            currentQuestionIndex++;
            ScoreText.Text = $"Score: {score}";
            ScoreProgressBar.Value = score;

            DisplayCurrentQuestion();
        }

        private void ShowFinalFeedback()
        {
            QuestionText.Text = "Quiz Completed!";
            OptionsPanel.Children.Clear();
            ScoreText.Text = $"Final Score: {score}/8";
            ScoreProgressBar.Value = score;

            if (score == 8)
                FeedbackText.Text = "🎉 You are a Champ!";
            else if (score > 6)
                FeedbackText.Text = "✅ You did well, keep it up!";
            else if (score > 4)
                FeedbackText.Text = "👍 You did great!";
            else
                FeedbackText.Text = "❌ Not a good performance. Try again!";
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public int CorrectOption { get; set; }

        public Question(string text, string[] options, int correctOption)
        {
            Text = text;
            Options = options;
            CorrectOption = correctOption;
        }
    }
}

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.IO;
using ChatBotApplication.Views;


namespace ChatBotApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tag)
            {
                switch (tag)
                {
                    case "TaskView":
                        SharedActivityLog.Instance.AddEntry("Visited the Task Assistant page", "Task");
                        MainFrame.Content = new Views.TaskView();
                        break;
                    case "QuizView":
                        SharedActivityLog.Instance.AddEntry("Opened the Quiz Mini-Game", "Quiz");
                        MainFrame.Content = new Views.QuizView();
                        break;
                    case "ChatView":
                        SharedActivityLog.Instance.AddEntry("Entered the Chat Assistant", "Chat");
                        MainFrame.Content = new Views.ChatView();
                        break;
                    case "ActivityLogView":
                        SharedActivityLog.Instance.AddEntry("Viewed the Activity Log summary", "Log");
                        MainFrame.Content = new Views.ActivityLogView();
                        break;
                }
            }
        }


        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}


using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ChatBotApplication.Views
{
    public partial class ActivityLogView : UserControl
    {
        public ObservableCollection<ActivityLogEntry> ActivityLog { get; set; }

        public ActivityLogView()
        {
            InitializeComponent();
            ActivityLog = SharedActivityLog.Instance.LogEntries;
            DataContext = this;
        }

        private void ClearLog_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all activity logs?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SharedActivityLog.Instance.Clear();
            }
        }

        private void FilterLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filter = (e.AddedItems[0] as ComboBoxItem)?.Content?.ToString();
            ActivityLog = SharedActivityLog.Instance.FilterByCategory(filter);
            DataContext = null;
            DataContext = this;
        }
    }

    public class ActivityLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } // Task, Quiz, Chat
    }

    public class SharedActivityLog
    {
        private static SharedActivityLog _instance;
        public static SharedActivityLog Instance => _instance ??= new SharedActivityLog();
        public ObservableCollection<ActivityLogEntry> LogEntries { get; set; }

        private SharedActivityLog()
        {
            LogEntries = new ObservableCollection<ActivityLogEntry>();
        }

        public void AddEntry(string description, string category = "General")
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                LogEntries.Add(new ActivityLogEntry
                {
                    Timestamp = DateTime.Now,
                    Description = description,
                    Category = category
                });
            });
        }

        public void Clear()
        {
            Application.Current.Dispatcher.Invoke(() => LogEntries.Clear());
        }

        public ObservableCollection<ActivityLogEntry> FilterByCategory(string category)
        {
            if (string.IsNullOrEmpty(category) || category == "All")
                return LogEntries;

            var filtered = LogEntries.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            return new ObservableCollection<ActivityLogEntry>(filtered);
        }

    }

    // --- Example usage in TaskView.cs when a task is added ---
    // SharedActivityLog.Instance.AddEntry($"Task added: '{taskTitle}' (Reminder set for {reminderDate:MMMM dd, yyyy})", "Task");

    // --- Example usage in QuizView.cs when a quiz is completed ---
    // SharedActivityLog.Instance.AddEntry($"Quiz completed – {questionsAnswered} questions answered", "Quiz");

    // --- Example usage in ChatView.cs when a topic is handled ---
    // SharedActivityLog.Instance.AddEntry($"Cyber topic discussed: {selectedTopic}", "Chat");
}

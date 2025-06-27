using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ChatBotApplication.Views
{
    public partial class TaskView : UserControl
    {
        public ObservableCollection<Task> Tasks { get; set; } = new ObservableCollection<Task>();

        public TaskView()
        {
            InitializeComponent();
            TaskListView.ItemsSource = Tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskTitleBox.Text))
            {
                MessageBox.Show("Please enter a title for the task.");
                return;
            }

            Task newTask = new Task
            {
                Title = TaskTitleBox.Text.Trim(),
                Description = TaskDescriptionBox.Text.Trim(),
                ReminderDate = ReminderDatePicker.SelectedDate ?? DateTime.Today
            };

            Tasks.Add(newTask);

            // Clear input fields
            TaskTitleBox.Text = string.Empty;
            TaskDescriptionBox.Text = string.Empty;
            ReminderDatePicker.SelectedDate = null;

            // Show success popup
            MessageBox.Show("Task details successfully saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Show success popup
        MessageBox.Show("Task details successfully saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is Task selectedTask)
            {
                var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Tasks.Remove(selectedTask);
                }
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is Task selectedTask)
            {
                // Populate fields with existing task info
                TaskTitleBox.Text = selectedTask.Title;
                TaskDescriptionBox.Text = selectedTask.Description;
                ReminderDatePicker.SelectedDate = selectedTask.ReminderDate;

                // Remove the original so the updated version can be added again
                Tasks.Remove(selectedTask);
            }
        }
    }
    }

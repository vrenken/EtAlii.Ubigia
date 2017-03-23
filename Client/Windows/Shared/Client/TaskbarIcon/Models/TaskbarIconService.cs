namespace EtAlii.Ubigia.Client.Windows.TaskbarIcon
{
    using System.Windows;

    public class TaskbarIconService : ITaskbarIconService
    {
        public Hardcodet.Wpf.TaskbarNotification.TaskbarIcon TaskbarIcon { get; }

        public TaskbarIconService(TaskbarIcon taskbarIcon)
        {
            TaskbarIcon = taskbarIcon;
        }

        public void Start()
        {
            TaskbarIcon.Dispatcher.Invoke(() =>
            {
                TaskbarIcon.Visibility = Visibility.Visible;
            });
        }

        public void Stop()
        {
            TaskbarIcon.Dispatcher.Invoke(() =>
            {
                TaskbarIcon.Visibility = Visibility.Collapsed;
            });
        }
    }
}
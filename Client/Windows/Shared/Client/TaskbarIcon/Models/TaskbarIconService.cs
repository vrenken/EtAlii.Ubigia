namespace EtAlii.Ubigia.Client.Windows.TaskbarIcon
{
    using System.Windows;

    public class TaskbarIconService : ITaskbarIconService
    {
        public Hardcodet.Wpf.TaskbarNotification.TaskbarIcon TaskbarIcon { get { return _taskbarIcon; } }
        private readonly Hardcodet.Wpf.TaskbarNotification.TaskbarIcon _taskbarIcon;

        public TaskbarIconService(TaskbarIcon taskbarIcon)
        {
            _taskbarIcon = taskbarIcon;
        }

        public void Start()
        {
            _taskbarIcon.Dispatcher.Invoke(() =>
            {
                _taskbarIcon.Visibility = Visibility.Visible;
            });
        }

        public void Stop()
        {
            _taskbarIcon.Dispatcher.Invoke(() =>
            {
                _taskbarIcon.Visibility = Visibility.Collapsed;
            });
        }
    }
}
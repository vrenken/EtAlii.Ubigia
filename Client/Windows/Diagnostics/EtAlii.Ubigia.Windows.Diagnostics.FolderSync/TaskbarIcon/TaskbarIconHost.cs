namespace EtAlii.Ubigia.Diagnostics.FolderSync
{
    using System.Threading.Tasks;
    using System.Windows;

    internal class TaskbarIconHost
    {
        public TaskbarIcon TaskbarIcon { get { return _taskbarIcon; } }
        private readonly TaskbarIcon _taskbarIcon;
        private readonly FolderMonitorManager _folderMonitorManager;

        public TaskbarIconHost(
            TaskbarIcon taskbarIcon,
            TaskbarIconViewModel taskbarIconViewModel,
            FolderMonitorManager folderMonitorManager)
            : base()
        {
            _taskbarIcon = taskbarIcon;
            _folderMonitorManager = folderMonitorManager;
            _taskbarIcon.DataContext = taskbarIconViewModel;
        }

        public void Start()
        {
            _taskbarIcon.Dispatcher.Invoke(() =>
            {
                _taskbarIcon.Visibility = Visibility.Visible;
            });
            Task.Delay(500).ContinueWith((o) => _folderMonitorManager.Start());
        }

        //public void Stop()
        //{
        //    Task.Delay(500).ContinueWith((o) => _fileSystemMonitor.Stop());
        //}
    }
}

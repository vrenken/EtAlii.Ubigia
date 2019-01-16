namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Threading.Tasks;
    using System.Windows;

    internal class TaskbarIconHost : ITaskbarIconHost
    {
        public ITaskbarIcon TaskbarIcon { get; }

        private readonly IFolderMonitorManager _folderMonitorManager;

        public TaskbarIconHost(
            ITaskbarIcon taskbarIcon,
            ITaskbarIconViewModel taskbarIconViewModel,
            IFolderMonitorManager folderMonitorManager)
        {
            TaskbarIcon = taskbarIcon;
            _folderMonitorManager = folderMonitorManager;
            TaskbarIcon.DataContext = taskbarIconViewModel;
        }

        public void Start()
        {
            TaskbarIcon.Dispatcher.Invoke(() =>
            {
                TaskbarIcon.Visibility = Visibility.Visible;
            });
            Task.Delay(500).ContinueWith((o) => _folderMonitorManager.Start());
        }

        //public void Stop()
        //{
        //    Task.Delay(500).ContinueWith((o) => _fileSystemMonitor.Stop());
        //}
    }
}

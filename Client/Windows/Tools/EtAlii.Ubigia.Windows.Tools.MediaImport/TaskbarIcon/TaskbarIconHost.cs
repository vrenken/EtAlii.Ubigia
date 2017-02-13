namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Threading.Tasks;
    using System.Windows;

    internal class TaskbarIconHost : ITaskbarIconHost
    {
        public ITaskbarIcon TaskbarIcon { get { return _taskbarIcon; } }
        private readonly ITaskbarIcon _taskbarIcon;
        private readonly IFolderMonitorManager _folderMonitorManager;

        public TaskbarIconHost(
            ITaskbarIcon taskbarIcon,
            ITaskbarIconViewModel taskbarIconViewModel,
            IFolderMonitorManager folderMonitorManager)
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

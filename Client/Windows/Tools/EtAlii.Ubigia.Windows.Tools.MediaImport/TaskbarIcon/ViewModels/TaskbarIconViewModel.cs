namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.xTechnology.Mvvm;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Ubigia.Windows;
    using EtAlii.xTechnology.MicroContainer;

    internal class TaskbarIconViewModel : BindableBase, ITaskbarIconViewModel
    {
        private readonly Container _container;
        private readonly IFolderMonitorManager _folderMonitorManager;

        public string ToolTipText { get { return _toolTipText; } set { SetProperty(ref _toolTipText, value); } }
        private string _toolTipText;

        public ICommand ExitApplicationCommand { get; }

        public bool CanShowConfiguration { get { return _canShowConfiguration; } set { SetProperty(ref _canShowConfiguration, value); } }
        private bool _canShowConfiguration;

        public ICommand ShowConfigurationCommand { get; }

        public bool CanShowStatus { get { return _canShowStatus; } set { SetProperty(ref _canShowStatus, value); } }
        private bool _canShowStatus;

        public ICommand ShowStatusCommand { get; }

        public bool CanShowAbout { get { return _canShowAbout; } set { SetProperty(ref _canShowAbout, value); } }
        private bool _canShowAbout;

        public ICommand ShowAboutCommand { get; }

        public string IconToShow { get { return _iconToShow; } set { SetProperty(ref _iconToShow, value); } }
        private string _iconToShow = TaskbarIconResource.Stopped;

        public TaskbarIconViewModel(Container container, IFolderMonitorManager folderMonitorManager)
        {
            _container = container;
            _folderMonitorManager = folderMonitorManager;
            _folderMonitorManager.PropertyChanged += OnMonitorManagerPropertyChanged;

            var sb = new StringBuilder();
            
            sb.AppendLine("Ubigia MediaImport");
            //sb.AppendLine();
            //sb.AppendFormat("Address: {0}", configuration.Address);
            ToolTipText = sb.ToString();

            ExitApplicationCommand = new RelayCommand(o =>
            {
                _folderMonitorManager.Stop();
                Application.Current.Shutdown();
            });
            ShowConfigurationCommand = new RelayCommand(ShowConfiguration);
            ShowStatusCommand = new RelayCommand(ShowStatus);
            ShowAboutCommand = new RelayCommand(ShowAbout);
        }

        void OnMonitorManagerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanShowConfiguration = _folderMonitorManager.IsRunning;
            CanShowStatus = _folderMonitorManager.IsRunning;
            CanShowAbout = _folderMonitorManager.IsRunning;

            switch (e.PropertyName)
            {
                case "IsRunning":
                    IconToShow = _folderMonitorManager.IsRunning ? TaskbarIconResource.Running : TaskbarIconResource.Stopped;
                    break;
                case "HasError":
                    if (_folderMonitorManager.HasError)
                    {
                        IconToShow = TaskbarIconResource.Errored;
                    }
                    else
                    {
                        IconToShow = _folderMonitorManager.IsRunning ? TaskbarIconResource.Running : TaskbarIconResource.Stopped;
                    }
                    break;
            }
        }

        private void ShowAbout(object obj)
        {
        }

        private void ShowStatus(object obj)
        {
        }

        private void ShowConfiguration(object obj)
        {
            var window = _container.GetInstance<IConfigurationWindow>();
            window.Show();
        }

    }
}

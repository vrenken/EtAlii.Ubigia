namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.Mvvm;
    using System.Text;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;

    public class TaskbarIconViewModel : BindableBase, ITaskbarIconViewModel
    {
        private ITrayIconProviderHost _providerHost;

        public string ToolTipText
        {
            get { return _toolTipText; }
            set { SetProperty(ref _toolTipText, value); }
        }

        private string _toolTipText;

        public ICommand ExitApplicationCommand { get; }

        public ICommand StartServiceCommand { get; }

        public ICommand StopServiceCommand { get; }

        public bool CanStartService
        {
            get { return _canStartService; }
            set { SetProperty(ref _canStartService, value); }
        }

        private bool _canStartService;

        public bool CanStopService
        {
            get { return _canStopService; }
            set { SetProperty(ref _canStopService, value); }
        }

        private bool _canStopService;


        public TaskbarIconViewModel()
        {
            StopServiceCommand = new RelayCommand(o => _providerHost.Stop());
            StartServiceCommand = new RelayCommand(o => _providerHost.Start());
            ExitApplicationCommand = new RelayCommand(o =>
            {
                _providerHost.Stop();
                Application.Current.Shutdown();
            });
        }

        public void Initialize(ITrayIconProviderHost providerHost)
        {
            _providerHost = providerHost;
            _providerHost.PropertyChanged += OnHostPropertyChanged;
            SetIcon(TrayIconResource.Stopped);
        }

        private void UpdateToolTip()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Ubigia provider");
            sb.AppendLine();
            sb.AppendFormat("Address: {0}", _providerHost.Configuration.Address);

            if (_providerHost.HasError)
            {
                sb.AppendLine();
                sb.Append("(In error)");
            }
            else if (!_providerHost.IsRunning)
            {
                sb.AppendLine();
                sb.Append("(Not running)");
            }
            ToolTipText = sb.ToString();
        }

        private void UpdateIcon()
        {
            var iconToShow = _providerHost.IsRunning ? TrayIconResource.Running : TrayIconResource.Stopped;
            SetIcon(iconToShow);
        }
        private void SetIcon(string iconFile)
        {
            var current = _providerHost.TaskbarIcon.Icon;

            var type = typeof(TaskbarIconViewModel);
            var resourceNamespace = Path.GetFileNameWithoutExtension(type.Assembly.CodeBase);
            using (var stream = type.Assembly.GetManifestResourceStream($"{resourceNamespace}.{iconFile}"))
            {
                _providerHost.TaskbarIcon.Icon = new System.Drawing.Icon(stream);
            }
            current?.Dispose();

        }

        private void OnHostPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_providerHost.IsRunning):
                    CanStopService = _providerHost.IsRunning;
                    CanStartService = !_providerHost.IsRunning;
                    UpdateIcon();
                    UpdateToolTip();
                    break;
                case nameof(_providerHost.HasError):
                    if (_providerHost.HasError)
                    {
                        SetIcon(TrayIconResource.Errored);
                    }
                    else
                    {
                        UpdateIcon();
                    }
                    break;
            }
        }
    }
}

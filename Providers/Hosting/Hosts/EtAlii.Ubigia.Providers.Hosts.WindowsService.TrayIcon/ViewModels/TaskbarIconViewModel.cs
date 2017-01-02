namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.Mvvm;
    using System.Text;
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

        public ICommand ExitApplicationCommand
        {
            get { return _exitApplicationCommand; }
        }

        private readonly ICommand _exitApplicationCommand;

        public ICommand StartServiceCommand
        {
            get { return _startServiceCommand; }
        }

        private readonly ICommand _startServiceCommand;

        public ICommand StopServiceCommand
        {
            get { return _stopServiceCommand; }
        }

        private readonly ICommand _stopServiceCommand;

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

        public string IconToShow
        {
            get { return _iconToShow; }
            set { SetProperty(ref _iconToShow, value); }
        }

        private string _iconToShow = TrayIconResource.Stopped;

        public TaskbarIconViewModel()
        {
            _stopServiceCommand = new RelayCommand(o => _providerHost.Stop());
            _startServiceCommand = new RelayCommand(o => _providerHost.Start());
            _exitApplicationCommand = new RelayCommand(o =>
            {
                _providerHost.Stop();
                Application.Current.Shutdown();
            });
        }

        public void Initialize(ITrayIconProviderHost providerHost)
        {
            _providerHost = providerHost;
            _providerHost.PropertyChanged += OnHostPropertyChanged;
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

        private void OnHostPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsRunning":
                    CanStopService = _providerHost.IsRunning;
                    CanStartService = !_providerHost.IsRunning;
                    IconToShow = _providerHost.IsRunning ? TrayIconResource.Running : TrayIconResource.Stopped;
                    UpdateToolTip();
                    break;
                case "HasError":
                    if (_providerHost.HasError)
                    {
                        IconToShow = TrayIconResource.Errored;
                    }
                    else
                    {
                        IconToShow = _providerHost.IsRunning ? TrayIconResource.Running : TrayIconResource.Stopped;
                    }
                    break;
            }
        }
    }
}

namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.Mvvm;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Ubigia.Infrastructure.Hosting.Properties;

    internal class TaskbarIconViewModel : BindableBase
    {
        private ITrayIconHost _host;

        public string ToolTipText { get { return _toolTipText; } private set { SetProperty(ref _toolTipText, value); } }
        private string _toolTipText;

        public ICommand ExitApplicationCommand { get { return _exitApplicationCommand; } }
        private readonly ICommand _exitApplicationCommand;

        public ICommand StartServiceCommand { get { return _startServiceCommand; } }
        private readonly ICommand _startServiceCommand;

        public ICommand StopServiceCommand { get { return _stopServiceCommand; } }
        private readonly ICommand _stopServiceCommand;

        public ICommand StorageBrowserCommand { get { return _storageBrowserCommand; } }
        private readonly ICommand _storageBrowserCommand;

        public ICommand SpaceBrowserCommand { get { return _spaceBrowserCommand; } }
        private readonly ICommand _spaceBrowserCommand;

        public ICommand OpenAdminPortalCommand { get { return _openAdminPortalCommand; } }
        private readonly ICommand _openAdminPortalCommand;

        public ICommand OpenUserPortalCommand { get { return _openUserPortalCommand; } }
        private readonly ICommand _openUserPortalCommand;

        public bool CanStartService { get { return _canStartService; } set { SetProperty(ref _canStartService, value); } }
        private bool _canStartService;

        public bool CanStopService { get { return _canStopService; } set { SetProperty(ref _canStopService, value); } }
        private bool _canStopService;

        public string IconToShow { get { return _iconToShow; } set { SetProperty(ref _iconToShow, value); } }
        private string _iconToShow = TrayIconResource.Stopped;

        public TaskbarIconViewModel()
        {
            _stopServiceCommand = new RelayCommand(o => StopHost());
            _startServiceCommand = new RelayCommand(o => StartHost());
            _exitApplicationCommand = new RelayCommand(o => Shutdown());
            _storageBrowserCommand = new RelayCommand(o => StartStorageBrowser());
            _spaceBrowserCommand = new RelayCommand(o => StartSpaceBrowser());

            _openAdminPortalCommand = new RelayCommand(o => Process.Start(new ProcessStartInfo(_host.Infrastructure.Configuration.Address + "/Admin") { UseShellExecute = true }));
            _openUserPortalCommand = new RelayCommand(o => Process.Start(new ProcessStartInfo(_host.Infrastructure.Configuration.Address + "/User") { UseShellExecute = true }));
        }

        public void Initialize(ITrayIconHost host)
        {
            _host = host;
            _host.PropertyChanged += OnHostPropertyChanged;
        }

        private void UpdateToolTip()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Ubigia infastructure host");
            sb.AppendLine();
            sb.AppendFormat("Address: {0}", _host.Infrastructure.Configuration.Address);

            if (_host.HasError)
            {
                sb.AppendLine();
                sb.Append("(In error)");
            }
            else if (!_host.IsRunning)
            {
                sb.AppendLine();
                sb.Append("(Not running)");
            }
            ToolTipText = sb.ToString();
        }


        void OnHostPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsRunning":
                    CanStopService = _host.IsRunning;
                    CanStartService = !_host.IsRunning;
                    IconToShow = _host.IsRunning ? TrayIconResource.Running : TrayIconResource.Stopped;
                    UpdateToolTip();
                    break;
                case "HasError":
                    if (_host.HasError)
                    {
                        IconToShow = TrayIconResource.Errored;
                    }
                    else
                    {
                        IconToShow = _host.IsRunning ? TrayIconResource.Running : TrayIconResource.Stopped;
                    }
                    break;
            }
        }

        private void StartSpaceBrowser()
        {
            StartProcess(Settings.Default.SpaceBrowserPath, _host.Infrastructure.Configuration.Address);
        }

        private void StartStorageBrowser()
        {
            StartProcess(Settings.Default.StorageBrowserPath, _host.Infrastructure.Configuration.Address);
        }

        private void StartHost()
        {
            _host.Start();
        }

        private void StopHost()
        {
            _host.Stop();
        }

        private void StartProcess(string fileName, string arguments = "")
        {
            fileName = Path.Combine(Environment.CurrentDirectory, fileName);
            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = true,
            };
            Process.Start(startInfo);
        }

        private void Shutdown()
        {
            _host.Stop();
            Application.Current.Shutdown();
        }
    }
}

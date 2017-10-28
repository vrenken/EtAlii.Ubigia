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

    internal class TaskbarIconViewModel : BindableBase, ITaskbarIconViewModel
    {
        private ITrayIconHost _host;

        public string ToolTipText { get { return _toolTipText; } private set { SetProperty(ref _toolTipText, value); } }
        private string _toolTipText;

        public ICommand ExitApplicationCommand { get; }

        public ICommand StartServiceCommand { get; }

        public ICommand StopServiceCommand { get; }

        public ICommand StorageBrowserCommand { get; }

        public ICommand SpaceBrowserCommand { get; }

        public ICommand OpenAdminPortalCommand { get; }

        public ICommand OpenUserPortalCommand { get; }

        public bool CanStartService { get => _canStartService; set => SetProperty(ref _canStartService, value); }
        private bool _canStartService;

        public bool CanStopService { get => _canStopService; set => SetProperty(ref _canStopService, value); }
        private bool _canStopService;

        public TaskbarIconViewModel()
        {
            StopServiceCommand = new RelayCommand(o => StopHost());
            StartServiceCommand = new RelayCommand(o => StartHost());
            ExitApplicationCommand = new RelayCommand(o => Shutdown());
            StorageBrowserCommand = new RelayCommand(o => StartStorageBrowser());
            SpaceBrowserCommand = new RelayCommand(o => StartSpaceBrowser());

            OpenAdminPortalCommand = new RelayCommand(o => BrowseTo("/Admin"));
            OpenUserPortalCommand = new RelayCommand(o => BrowseTo("/"));
        }

        private void BrowseTo(string relativeAddress)
        {
            var hostAddress = _host.Infrastructure.Configuration.Address.Replace("+", "localhost");
            Process.Start(new ProcessStartInfo(hostAddress + relativeAddress) {UseShellExecute = true});
        }

        public void Initialize(ITrayIconHost host)
        {
            _host = host;
            _host.PropertyChanged += OnHostPropertyChanged;
            SetIcon(TrayIconResource.Stopped);
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
                case nameof(_host.IsRunning):
                    CanStopService = _host.IsRunning;
                    CanStartService = !_host.IsRunning;
                    UpdateIcon();
                    UpdateToolTip();
                    break;
                case nameof(_host.HasError):
                    if (_host.HasError)
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

        private void UpdateIcon()
        {
            var iconToShow = _host.IsRunning ? TrayIconResource.Running : TrayIconResource.Stopped;
            SetIcon(iconToShow);
        }
        private void SetIcon(string iconFile)
        {
            var current = _host.TaskbarIcon.Icon;

            var type = typeof(TaskbarIconViewModel);
            var resourceNamespace = Path.GetFileNameWithoutExtension(type.Assembly.CodeBase);
            using (var stream = type.Assembly.GetManifestResourceStream($"{resourceNamespace}.{iconFile}"))
            {
                _host.TaskbarIcon.Icon = new System.Drawing.Icon(stream);
            }
            current?.Dispose();

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

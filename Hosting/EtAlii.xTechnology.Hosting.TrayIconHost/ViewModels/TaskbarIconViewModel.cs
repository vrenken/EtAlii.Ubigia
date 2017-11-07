namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.Mvvm;
    using System.Text;
    using System.Windows;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using System.Drawing;

    internal partial class TaskbarIconViewModel : BindableBase, ITaskbarIconViewModel
    {
        private readonly IInfrastructure _infrastructure;
        private readonly IHostCommandsConverter _hostCommandsConverter;
        private ITrayIconHost _host;
        private Icon _runningIcon;
        private Icon _stoppedIcon;
        private Icon _errorIcon;

        public string ToolTipText { get => _toolTipText; private set => SetProperty(ref _toolTipText, value); }
        private string _toolTipText;

        public MenuItemViewModel[] MenuItems { get => _menuItems; private set => SetProperty(ref _menuItems, value); }
        private MenuItemViewModel[] _menuItems = new MenuItemViewModel[0];

        public TaskbarIconViewModel(
            IInfrastructure infrastructure,
            IHostCommandsConverter hostCommandsConverter)
        {
            _infrastructure = infrastructure;
            _hostCommandsConverter = hostCommandsConverter;
        }

        public void Initialize(ITrayIconHost host, Icon runningIcon, Icon stoppedIcon, Icon errorIcon)
        {
            _host = host;
            _host.PropertyChanged += OnHostPropertyChanged;
            _host.StatusChanged += OnHostStatusChanged;

            _runningIcon = runningIcon;
            _stoppedIcon = stoppedIcon;
            _errorIcon = errorIcon;

            SetIcon(_stoppedIcon);

            MenuItems = _hostCommandsConverter.ToViewModels(_host.Commands);
        }


        private void UpdateToolTip()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Ubigia infastructure host");
            sb.AppendLine();
            sb.AppendFormat("Address: {0}", _infrastructure.Configuration.Address);

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

                    UpdateIcon();
                    UpdateToolTip();
                    break;
                case nameof(_host.HasError):
                    if (_host.HasError)
                    {
                        SetIcon(_errorIcon);
                    }
                    else
                    {
                        UpdateIcon();
                    }
                    break;
            }
        }

        private void OnHostStatusChanged(HostStatus status)
        {
            switch (status)
            {
                case HostStatus.Shutdown:
                    Application.Current.Shutdown();
                    break;
            }
            ;
        }

        private void UpdateIcon()
        {
            var iconToShow = _host.IsRunning ? _runningIcon : _stoppedIcon;
            SetIcon(iconToShow);
        }
        private void SetIcon(Icon icon)
        {
            _host.TaskbarIcon.Icon = icon;
        }
    }
}

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.Mvvm;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class TaskbarIconViewModel : BindableBase, ITaskbarIconViewModel
    {
        private readonly IInfrastructure _infrastructure;
        private ITrayIconHost _host;

        public string ToolTipText { get => _toolTipText; private set => SetProperty(ref _toolTipText, value); }
        private string _toolTipText;

        public MenuItemViewModel[] MenuItems { get => _menuItems; private set => SetProperty(ref _menuItems, value); }
        private MenuItemViewModel[] _menuItems = new MenuItemViewModel[0];

        public bool CanStartService { get => _canStartService; set => SetProperty(ref _canStartService, value); }
        private bool _canStartService;

        public bool CanStopService { get => _canStopService; set => SetProperty(ref _canStopService, value); }
        private bool _canStopService;

        public TaskbarIconViewModel(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public void Initialize(ITrayIconHost host)
        {
            _host = host;
            _host.PropertyChanged += OnHostPropertyChanged;
            _host.StatusChanged += OnHostStatusChanged;
            SetIcon(TrayIconResource.Stopped);

            MenuItems = ToViewModels(_host.Commands);
            

            //MenuItems = new []
            //{
            //    new MenuItemViewModel("About"),
            //    new MenuItemViewModel("User API service", new []
            //    {
            //        new MenuItemViewModel("Start"),
            //        new MenuItemViewModel("Stop"),
            //        new MenuItemViewModel("Configure"),
            //    }),
            //    new MenuItemViewModel("Admin API service", new []
            //    {
            //        new MenuItemViewModel("Start"),
            //        new MenuItemViewModel("Stop"),
            //        new MenuItemViewModel("Configure"),
            //    }),
            //    new MenuItemViewModel("Start service", StartServiceCommand),
            //    new MenuItemViewModel("Stop service", StopServiceCommand),
            //    new MenuItemViewModel("Space browser", SpaceBrowserCommand),
            //    new MenuItemViewModel("Storage browser", StorageBrowserCommand),
            //    new MenuItemViewModel("User portal", OpenUserPortalCommand),
            //    new MenuItemViewModel("Admin portal", OpenAdminPortalCommand),
            //    new MenuItemViewModel("Exit", ExitApplicationCommand),
            //};
        }

 
        private MenuItemViewModel[] ToViewModels(IHostCommand[] commands)
        {
            var result = new List<MenuItemViewModel>();

            foreach (var command in commands)
            {
                var nameParts = command.Name.Split('/');

                MenuItemViewModel parent = null;

                for (int i = 0; i < nameParts.Length; i++)
                {
                    var collectionToAddTo = (IList<MenuItemViewModel>) parent?.Items ?? result;
                    if (i == nameParts.Length - 1)
                    {
                        var menuItem = new MenuItemViewModel(nameParts[i], new WrappedHostCommand(command));
                        collectionToAddTo.Add(menuItem);
                    }
                    else
                    {
                        var newHeader = nameParts[i];
                        var menuItem = collectionToAddTo.FirstOrDefault(item => item.Header == newHeader);
                        if (menuItem == null)
                        {
                            menuItem = new MenuItemViewModel(newHeader);
                            collectionToAddTo.Add(menuItem);
                        }
                        parent = menuItem;
                    }
                }
            }
            return result.ToArray();
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
                        SetIcon(TrayIconResource.Errored);
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
    }
}

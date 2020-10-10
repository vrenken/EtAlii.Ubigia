namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Windows;

    public partial class TrayIconHost : HostBase, ITrayIconHost
    {
        public ITaskbarIcon TaskbarIcon { get; }

        public TrayIconHost(
            IHostConfiguration configuration,
            ISystemManager systemManager,
            ITaskbarIcon taskbarIcon)
            : base(configuration, systemManager)
        {
            TaskbarIcon = taskbarIcon;

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(State):
                    if (State == State.Starting)
                    {
                        TaskbarIcon.Dispatcher.Invoke(() => TaskbarIcon.Visibility = Visibility.Visible);
                    }
                    break;
            }
        }
    }
}

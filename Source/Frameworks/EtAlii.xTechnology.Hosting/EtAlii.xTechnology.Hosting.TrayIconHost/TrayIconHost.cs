// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Windows;

    public partial class TrayIconHost : HostBase, ITrayIconHost
    {
        public ITaskbarIcon TaskbarIcon { get; }

        public TrayIconHost(
            IHostOptions options,
            ISystemManager systemManager,
            ITaskbarIcon taskbarIcon)
            : base(options, systemManager)
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

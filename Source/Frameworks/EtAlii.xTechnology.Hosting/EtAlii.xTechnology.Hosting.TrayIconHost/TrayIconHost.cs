// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Windows;

    public abstract partial class TrayIconHost : HostBase, ITrayIconHost
    {
        public ITaskbarIcon TaskbarIcon { get; }

        protected TrayIconHost(IHostOptions options, ITaskbarIcon taskbarIcon)
            : base(options)
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

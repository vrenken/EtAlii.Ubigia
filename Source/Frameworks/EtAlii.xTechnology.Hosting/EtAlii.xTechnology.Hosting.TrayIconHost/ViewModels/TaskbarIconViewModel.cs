// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Drawing;
    using System.Text;
    using System.Windows;

    internal class TaskbarIconViewModel : BindableBase, ITaskbarIconViewModel
    {
        private readonly IHostCommandsConverter _hostCommandsConverter;
        private ITrayIconHost _host;
        private Icon _runningIcon;
        private Icon _stoppedIcon;
        private Icon _errorIcon;

        public string ToolTipText { get => _toolTipText; private set => SetProperty(ref _toolTipText, value); }
        private string _toolTipText;

        public MenuItemViewModel[] MenuItems { get => _menuItems; private set => SetProperty(ref _menuItems, value); }
        private MenuItemViewModel[] _menuItems = Array.Empty<MenuItemViewModel>();

        public TaskbarIconViewModel(IHostCommandsConverter hostCommandsConverter)
        {
            _hostCommandsConverter = hostCommandsConverter;
        }

        public void Initialize(ITrayIconHost host, Icon runningIcon, Icon stoppedIcon, Icon errorIcon)
        {
            _host = host;
            _host.PropertyChanged += OnHostPropertyChanged;

            _runningIcon = runningIcon;
            _stoppedIcon = stoppedIcon;
            _errorIcon = errorIcon;

            SetIcon(_stoppedIcon);

            MenuItems = _hostCommandsConverter.ToViewModels(_host.Commands);
        }


        private void UpdateToolTip()
        {
            var sb = new StringBuilder();

            sb.AppendLine();

            foreach (var status in _host.Status)
            {
                sb.AppendLine(status.Title);
                sb.AppendLine();
                sb.AppendLine(status.Summary);
                sb.AppendLine();
            }
            ToolTipText = sb.ToString();
        }


        private void OnHostPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_host.Status):
                    UpdateToolTip();
                    break;
                case nameof(_host.State):
                    OnHostStateChanged(_host.State);
                    break;
                case nameof(_host.Commands):
                    MenuItems = _hostCommandsConverter.ToViewModels(_host.Commands);
                    break;
            }
        }

        private void OnHostStateChanged(State state)
        {
            UpdateIcon();

            switch (state)
            {
                case State.Shutdown:
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void UpdateIcon()
        {
            Icon iconToShow;
            switch (_host.State)
            {
                case State.Running:
                    iconToShow = _runningIcon;
                    break;
                case State.Error:
                    iconToShow = _errorIcon;
                    break;
                default:
                    iconToShow = _stoppedIcon;
                    break;
            }
            SetIcon(iconToShow);
        }
        private void SetIcon(Icon icon)
        {
            _host.TaskbarIcon.Icon = icon;
        }
    }
}

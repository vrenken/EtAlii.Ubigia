// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Drawing;

    public interface ITaskbarIconViewModel : INotifyPropertyChanged
    {
        string ToolTipText { get; }

        void Initialize(ITrayIconHost host, Icon runningIcon, Icon stoppedIcon, Icon errorIcon);
    }
}
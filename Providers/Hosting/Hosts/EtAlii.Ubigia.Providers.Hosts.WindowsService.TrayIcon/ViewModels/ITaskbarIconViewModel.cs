﻿namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System.ComponentModel;
    using System.Windows.Input;

    public interface ITaskbarIconViewModel : INotifyPropertyChanged
    {
        string ToolTipText { get; set; }
        ICommand ExitApplicationCommand { get; }
        ICommand StartServiceCommand { get; }
        ICommand StopServiceCommand { get; }
        bool CanStartService { get; set; }
        bool CanStopService { get; set; }
        void Initialize(ITrayIconProviderHost providerHost);
    }
}
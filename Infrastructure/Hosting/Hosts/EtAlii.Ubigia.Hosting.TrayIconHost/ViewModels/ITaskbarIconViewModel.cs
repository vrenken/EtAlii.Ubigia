using System.Windows.Input;

namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.ComponentModel;
    internal interface ITaskbarIconViewModel : INotifyPropertyChanged
    {
        bool CanStartService { get; set; }
        bool CanStopService { get; set; }
        ICommand ExitApplicationCommand { get; }
        string IconToShow { get; set; }
        ICommand OpenAdminPortalCommand { get; }
        ICommand OpenUserPortalCommand { get; }
        ICommand SpaceBrowserCommand { get; }
        ICommand StartServiceCommand { get; }
        ICommand StopServiceCommand { get; }
        ICommand StorageBrowserCommand { get; }
        string ToolTipText { get; }

        void Initialize(ITrayIconHost host);
    }
}
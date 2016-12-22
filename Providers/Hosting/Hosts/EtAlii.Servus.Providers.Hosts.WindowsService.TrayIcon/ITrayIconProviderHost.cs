namespace EtAlii.Servus.Provisioning.Hosting
{
    using System.ComponentModel;

    public interface ITrayIconProviderHost : IProviderHost, INotifyPropertyChanged
    {
        bool IsRunning { get; set; }
        bool HasError { get; set; }
        ITaskbarIcon TaskbarIcon { get; }
    }
}
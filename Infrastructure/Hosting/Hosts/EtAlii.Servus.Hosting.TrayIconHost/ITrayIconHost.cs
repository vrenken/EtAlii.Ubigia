namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System.ComponentModel;

    public interface ITrayIconHost : IHost, INotifyPropertyChanged
    {
        bool IsRunning { get; set; }
        bool HasError { get; set; }
        ITaskbarIcon TaskbarIcon { get; }
    }
}
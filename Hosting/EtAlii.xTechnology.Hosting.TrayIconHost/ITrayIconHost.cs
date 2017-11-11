namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;

    public interface ITrayIconHost : IHost
    {
        ITaskbarIcon TaskbarIcon { get; }
    }
}
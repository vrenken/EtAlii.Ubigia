namespace EtAlii.xTechnology.Hosting
{
    public interface ITrayIconHost : IHost
    {
        ITaskbarIcon TaskbarIcon { get; }
    }
}
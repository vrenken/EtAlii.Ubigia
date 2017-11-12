namespace EtAlii.xTechnology.Hosting
{
    public enum HostState
    {
        Shutdown = 0,
        Starting,
        Running,
        Stopping,
        Stopped,
        Error,
    }
}
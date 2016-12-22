namespace EtAlii.Servus.Provisioning.Hosting
{
    public interface IWindowsServiceHostConfiguration : IHostConfiguration
    {
        string Name { get; }
    }
}
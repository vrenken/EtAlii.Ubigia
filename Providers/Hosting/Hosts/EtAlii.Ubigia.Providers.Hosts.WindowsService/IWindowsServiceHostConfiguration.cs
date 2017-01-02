namespace EtAlii.Ubigia.Provisioning.Hosting
{
    public interface IWindowsServiceHostConfiguration : IHostConfiguration
    {
        string Name { get; }
    }
}
namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;
    using SimpleInjector;

    public interface IInitializableInfrastructure
    {
        void Initialize(Container container, IStorageConfiguration configuration, IDiagnosticsConfiguration diagnostics);

        IHostingConfiguration CreateHostingConfiguration();
    }
}

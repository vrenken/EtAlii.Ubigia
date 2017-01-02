namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;
    using SimpleInjector;

    public interface IInitializableStorageSystem
    {
        void Initialize(Container container, IStorageConfiguration configuration, IDiagnosticsConfiguration diagnostics);
    }
}

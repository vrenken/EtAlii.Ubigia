namespace EtAlii.Servus.Api.Fabric
{
    using EtAlii.Servus.Api.Transport;

    public interface IFabricContextConfiguration
    {
        IDataConnection Connection { get; }
        bool TraversalCachingEnabled { get; }

        IFabricContextExtension[] Extensions { get; }

        IFabricContextConfiguration Use(IDataConnection connection);
        IFabricContextConfiguration UseTraversalCaching(bool cachingEnabled);

        IFabricContextConfiguration Use(IFabricContextExtension[] extensions);
    }
}
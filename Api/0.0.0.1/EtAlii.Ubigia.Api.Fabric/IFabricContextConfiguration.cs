namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

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
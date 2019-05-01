namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public interface IFabricContextConfiguration : IConfiguration<IFabricContextExtension, FabricContextConfiguration>
    {
        IDataConnection Connection { get; }
        bool TraversalCachingEnabled { get; }

        IFabricContextConfiguration Use(IDataConnection connection);
        IFabricContextConfiguration UseTraversalCaching(bool cachingEnabled);
    }
}
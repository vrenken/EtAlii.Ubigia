namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public interface IFabricContextConfiguration : IConfiguration<IFabricContextExtension, FabricContextConfiguration>
    {
        IDataConnection Connection { get; }
        bool TraversalCachingEnabled { get; }

        FabricContextConfiguration Use(IDataConnection connection);
        FabricContextConfiguration UseTraversalCaching(bool cachingEnabled);
    }
}
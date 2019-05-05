namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public interface IFabricContextConfiguration : IConfiguration<FabricContextConfiguration>
    {
        IDataConnection Connection { get; }
        bool TraversalCachingEnabled { get; }
    }
}
namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface ILogicalContextConfiguration : IConfiguration<ILogicalContextExtension, LogicalContextConfiguration>
    {
        IFabricContext Fabric { get; }
        bool CachingEnabled { get; }

        ILogicalContextConfiguration Use(IFabricContext fabric);
        ILogicalContextConfiguration UseCaching(bool cachingEnabled);
    }
}
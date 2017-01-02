namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;

    public interface ILogicalContextConfiguration
    {
        IFabricContext Fabric { get; }
        ILogicalContextExtension[] Extensions { get; }
        bool CachingEnabled { get; }

        ILogicalContextConfiguration Use(IFabricContext fabric);
        ILogicalContextConfiguration Use(ILogicalContextExtension[] extensions);
        ILogicalContextConfiguration UseCaching(bool cachingEnabled);
    }
}
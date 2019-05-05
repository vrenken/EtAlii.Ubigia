namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface ILogicalContextConfiguration : IConfiguration
    {
        IFabricContext Fabric { get; }
        bool CachingEnabled { get; }
    }
}
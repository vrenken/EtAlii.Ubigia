namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface ILogicalContextConfiguration : IConfiguration<LogicalContextConfiguration>
    {
        IFabricContext Fabric { get; }
        bool CachingEnabled { get; }
    }
}
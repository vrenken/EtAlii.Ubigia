namespace EtAlii.Ubigia.Api.Logical
{
    public interface ILogicalContextConfiguration : IConfiguration
    {
        bool CachingEnabled { get; }
    }
}
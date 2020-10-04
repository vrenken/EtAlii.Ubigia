namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public interface IFabricContextConfiguration : IConfiguration
    {
        /// <summary>
        /// The Connection that should be used to communicate with the backend.
        /// </summary>
        IDataConnection Connection { get; }
        bool TraversalCachingEnabled { get; }
    }
}
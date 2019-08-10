namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public interface IEditableFabricContextConfiguration 
    {
        /// <summary>
        /// Gets or sets the Connection that should be used to communicate with the backend.
        /// </summary>
        IDataConnection Connection { get; set; }
        bool TraversalCachingEnabled { get; set; }
    }
}
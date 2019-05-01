namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public interface IEditableFabricContextConfiguration 
    {
        IDataConnection Connection { get; set; }
        bool TraversalCachingEnabled { get; set; }
    }
}
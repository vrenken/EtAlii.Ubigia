namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;

    public interface IContentManagerFactory
    {
        IContentManager Create(IFabricContext fabric);
    }
}
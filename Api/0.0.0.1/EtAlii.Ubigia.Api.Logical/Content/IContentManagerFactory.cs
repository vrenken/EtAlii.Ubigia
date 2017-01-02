namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface IContentManagerFactory
    {
        IContentManager Create(IFabricContext fabric);
    }
}
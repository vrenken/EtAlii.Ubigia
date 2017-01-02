namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;

    public interface IGraphComposerFactory
    {
        IGraphComposer Create(IFabricContext fabric);
    }
}
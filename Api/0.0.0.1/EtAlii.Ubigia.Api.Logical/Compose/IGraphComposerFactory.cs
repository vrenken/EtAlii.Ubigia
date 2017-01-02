namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface IGraphComposerFactory
    {
        IGraphComposer Create(IFabricContext fabric);
    }
}
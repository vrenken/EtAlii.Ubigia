namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface IGraphPathTraverserConfiguration
    {
        IFabricContext FabricContext { get; }

        IGraphPathTraverserExtension[] Extensions { get; }

        IGraphPathTraverserConfiguration Use(IFabricContext fabricContext);
        IGraphPathTraverserConfiguration Use(IGraphPathTraverserExtension[] extensions);
    }
}
namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;

    public interface IGraphPathTraverserConfiguration
    {
        IFabricContext FabricContext { get; }

        IGraphPathTraverserExtension[] Extensions { get; }

        IGraphPathTraverserConfiguration Use(IFabricContext fabricContext);
        IGraphPathTraverserConfiguration Use(IGraphPathTraverserExtension[] extensions);
    }
}
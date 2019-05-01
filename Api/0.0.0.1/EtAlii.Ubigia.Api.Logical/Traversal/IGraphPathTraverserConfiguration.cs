namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface IGraphPathTraverserConfiguration : IConfiguration<IGraphPathTraverserExtension, GraphPathTraverserConfiguration>
    {
        IFabricContext FabricContext { get; }

        IGraphPathTraverserConfiguration Use(IFabricContext fabricContext);
    }
}
namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface IGraphPathTraverserConfiguration : IConfiguration<IGraphPathTraverserExtension, GraphPathTraverserConfiguration>
    {
        IFabricContext FabricContext { get; }

        GraphPathTraverserConfiguration Use(IFabricContext fabricContext);
    }
}
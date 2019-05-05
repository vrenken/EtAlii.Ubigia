namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface IGraphPathTraverserConfiguration : IConfiguration<GraphPathTraverserConfiguration>
    {
        IFabricContext FabricContext { get; }

        GraphPathTraverserConfiguration Use(IFabricContext fabricContext);
    }
}
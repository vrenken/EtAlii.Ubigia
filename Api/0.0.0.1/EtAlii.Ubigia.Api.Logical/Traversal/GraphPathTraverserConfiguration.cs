namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public class GraphPathTraverserConfiguration : Configuration<IGraphPathTraverserExtension, GraphPathTraverserConfiguration>, IGraphPathTraverserConfiguration
    {
        public IFabricContext FabricContext { get; private set; }

        public IGraphPathTraverserConfiguration Use(IFabricContext fabricContext)
        {
            FabricContext = fabricContext;
            return this;
        }
    }
}
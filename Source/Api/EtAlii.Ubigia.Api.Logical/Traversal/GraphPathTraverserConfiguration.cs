namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public class GraphPathTraverserConfiguration : ConfigurationBase, IGraphPathTraverserConfiguration
    {
        public IFabricContext FabricContext { get; private set; }

        public GraphPathTraverserConfiguration Use(IFabricContext fabricContext)
        {
            FabricContext = fabricContext;
            return this;
        }
    }
}
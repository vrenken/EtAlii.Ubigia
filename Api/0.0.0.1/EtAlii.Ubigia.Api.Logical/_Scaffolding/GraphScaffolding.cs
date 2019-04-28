namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    internal class GraphScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IGraphPathBuilder, GraphPathBuilder>();
            container.Register<IGraphComposerFactory, GraphComposerFactory>();
            container.Register<IGraphComposer>(() =>
            {
                var fabric = container.GetInstance<IFabricContext>();
                var graphComposerFactory = container.GetInstance<IGraphComposerFactory>();
                return graphComposerFactory.Create(fabric);
            });

            container.Register<IGraphAssignerFactory, GraphAssignerFactory>();
            container.Register<IGraphAssigner>(() =>
            {
                var fabric = container.GetInstance<IFabricContext>();
                var graphAssignerFactory = container.GetInstance<IGraphAssignerFactory>();
                return graphAssignerFactory.Create(fabric);
            });

            container.Register<IGraphPathTraverserFactory, GraphPathTraverserFactory>();
            container.Register<IGraphPathTraverser>(() =>
            {
                var fabric = container.GetInstance<IFabricContext>();
                var configuration = new GraphPathTraverserConfiguration()
                    .Use(fabric);
                var graphPathTraverserFactory = container.GetInstance<IGraphPathTraverserFactory>();
                return graphPathTraverserFactory.Create(configuration);
            });

        }
    }
}
namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    public class GraphPathTraverserFactory : IGraphPathTraverserFactory
    {
        public GraphPathTraverserFactory()
        {
        }

        public IGraphPathTraverser Create(IGraphPathTraverserConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new TraversalScaffolding(configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IGraphPathTraverser>();
        }
    }
}

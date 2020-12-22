namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class GraphContextScaffolding : IScaffolding
    {
        private readonly GraphContextConfiguration _configuration;

        public GraphContextScaffolding(GraphContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IGraphContext, GraphContext>();
            container.Register<IGraphContextConfiguration>(() => _configuration);

            container.Register<ISchemaProcessorFactory, SchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, SchemaParserFactory>();

            container.Register(() => new TraversalScriptContextFactory().Create(_configuration));

            container.Register(() => new LogicalContextFactory().Create(_configuration));
        }
    }
}

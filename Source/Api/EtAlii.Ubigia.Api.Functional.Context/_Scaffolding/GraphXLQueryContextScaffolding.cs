namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class GraphXLQueryContextScaffolding : IScaffolding
    {
        private readonly GraphXLQueryContextConfiguration _configuration;

        public GraphXLQueryContextScaffolding(GraphXLQueryContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IGraphXLContext, GraphXLContext>();
            container.Register<IGraphXLQueryContextConfiguration>(() => _configuration);

            container.Register<ISchemaProcessorFactory, SchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, SchemaParserFactory>();

            container.Register(() => new TraversalScriptContextFactory().Create(_configuration));

            container.Register(() => new LogicalContextFactory().Create(_configuration));
        }
    }
}

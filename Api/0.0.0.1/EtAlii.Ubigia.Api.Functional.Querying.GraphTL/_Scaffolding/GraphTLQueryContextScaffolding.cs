namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class GraphTLQueryContextScaffolding : IScaffolding
    {
        private readonly GraphTLQueryContextConfiguration _configuration;

        public GraphTLQueryContextScaffolding(GraphTLQueryContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IGraphTLContext, GraphTLContext>();
            container.Register<IGraphTLQueryContextConfiguration>(() => _configuration);

            container.Register<ISchemaProcessorFactory, SchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, SchemaParserFactory>();
            
            container.Register<IGraphSLScriptContext>(() => new GraphSLScriptContextFactory().Create(_configuration));

            container.Register(() => new LogicalContextFactory().Create(_configuration));
        }
    }
}

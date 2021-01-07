namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaGraphContextExtension : IGraphContextExtension
    {
        private readonly GraphContextConfiguration _configuration;

        public LapaGraphContextExtension(GraphContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(Container container)
        {
            container.Register<IGraphContext, GraphContext>();
            container.Register<IGraphContextConfiguration>(() => _configuration);

            container.Register<ISchemaProcessorFactory, LapaSchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, LapaSchemaParserFactory>();

            container.Register(() => new TraversalScriptContextFactory().Create(_configuration));

            container.Register(() => new LogicalContextFactory().Create(_configuration));
        }
    }
}

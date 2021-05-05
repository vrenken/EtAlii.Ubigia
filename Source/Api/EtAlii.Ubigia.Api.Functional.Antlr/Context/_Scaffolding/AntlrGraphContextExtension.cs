namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class AntlrGraphContextExtension : IGraphContextExtension
    {
        private readonly GraphContextConfiguration _configuration;

        public AntlrGraphContextExtension(GraphContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(Container container)
        {
            container.Register<IGraphContext, GraphContext>();
            container.Register<IGraphContextConfiguration>(() => _configuration);

            container.Register<ISchemaProcessorFactory, AntlrSchemaProcessorFactory>();
            container.Register<ISchemaParserFactory, AntlrSchemaParserFactory>();

            container.Register(() => new TraversalContextFactory().Create(_configuration));

            container.Register(() => new LogicalContextFactory().Create(_configuration));
        }
    }
}

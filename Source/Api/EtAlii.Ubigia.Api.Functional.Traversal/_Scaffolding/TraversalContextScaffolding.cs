namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalContextScaffolding : IScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;
        private readonly TraversalParserConfiguration _parserConfiguration;
        private readonly TraversalProcessorConfiguration _processorConfiguration;
        private readonly TraversalContextConfiguration _configuration;

        public TraversalContextScaffolding(
            TraversalContextConfiguration configuration,
            TraversalParserConfiguration parserConfiguration,
            TraversalProcessorConfiguration processorConfiguration,
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _configuration = configuration;
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            _parserConfiguration = parserConfiguration;
            _processorConfiguration = processorConfiguration;
        }

        public void Register(Container container)
        {
            container.Register<ITraversalContext, TraversalContext>();
            container.Register<ITraversalContextConfiguration>(() => _configuration);
            container.Register<ITraversalParserConfiguration>(() => _parserConfiguration);
            container.Register<ITraversalProcessorConfiguration>(() => _processorConfiguration);
            container.Register(() => _functionHandlersProvider);
            container.Register(() => _rootHandlerMappersProvider);
            container.Register(() => new LogicalContextFactory().Create(_configuration));
            container.Register<IScriptParserFactory, ScriptParserFactory>();
            container.Register<IPathParserFactory, PathParserFactory>();
            container.Register<IScriptProcessorFactory, ScriptProcessorFactory>();
        }
    }
}

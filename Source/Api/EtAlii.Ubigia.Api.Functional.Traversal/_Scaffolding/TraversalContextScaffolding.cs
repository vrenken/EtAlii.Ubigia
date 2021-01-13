namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalContextScaffolding : IScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;
        private readonly Func<IScriptParserFactory> _scriptParserFactoryProvider;
        private readonly Func<IScriptProcessorFactory> _scriptProcessorFactoryProvider;
        private readonly Func<IPathParserFactory> _pathParserFactoryProvider;
        private readonly TraversalContextConfiguration _configuration;

        public TraversalContextScaffolding(
            TraversalContextConfiguration configuration,
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider,
            Func<IScriptParserFactory> scriptParserFactoryProvider,
            Func<IScriptProcessorFactory> scriptProcessorFactoryProvider,
            Func<IPathParserFactory> pathParserFactoryProvider)
        {
            _configuration = configuration;
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            _scriptParserFactoryProvider = scriptParserFactoryProvider;
            _scriptProcessorFactoryProvider = scriptProcessorFactoryProvider;
            _pathParserFactoryProvider = pathParserFactoryProvider;
        }

        public void Register(Container container)
        {
            container.Register<ITraversalContextConfiguration>(() => _configuration);
            container.Register(() => new LogicalContextFactory().Create(_configuration));
            container.Register<ITraversalContext, TraversalContext>();
            container.Register(() => _scriptProcessorFactoryProvider());
            container.Register(() => _scriptParserFactoryProvider());
            container.Register(() => _pathParserFactoryProvider);
            container.Register(() => _functionHandlersProvider);
            container.Register(() => _rootHandlerMappersProvider);
        }
    }
}

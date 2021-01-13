namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalContextScaffolding : IScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;
        private readonly TraversalParserConfiguration _parserConfiguration;
        private readonly Func<IScriptProcessorFactory> _scriptProcessorFactoryProvider;
        private readonly TraversalContextConfiguration _configuration;

        public TraversalContextScaffolding(
            TraversalContextConfiguration configuration,
            TraversalParserConfiguration parserConfiguration,
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider,
            Func<IScriptProcessorFactory> scriptProcessorFactoryProvider)
        {
            _configuration = configuration;
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            _parserConfiguration = parserConfiguration;
            _scriptProcessorFactoryProvider = scriptProcessorFactoryProvider;
        }

        public void Register(Container container)
        {
            container.Register(() => _parserConfiguration);
            container.Register<ITraversalContextConfiguration>(() => _configuration);
            container.Register(() => new LogicalContextFactory().Create(_configuration));
            container.Register<ITraversalContext, TraversalContext>();
            container.Register(() => _scriptProcessorFactoryProvider());
            container.Register(() => _functionHandlersProvider);
            container.Register(() => _rootHandlerMappersProvider);
        }
    }
}

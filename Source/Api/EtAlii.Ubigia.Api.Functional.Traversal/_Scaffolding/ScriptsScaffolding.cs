namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptsScaffolding : IScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;
        private readonly Func<IScriptParserFactory> _scriptParserFactoryProvider;
        private readonly Func<IScriptProcessorFactory> _scriptProcessorFactoryProvider;

        public ScriptsScaffolding(
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider,
            Func<IScriptParserFactory> scriptParserFactoryProvider,
            Func<IScriptProcessorFactory> scriptProcessorFactoryProvider)
        {
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            _scriptParserFactoryProvider = scriptParserFactoryProvider;
            _scriptProcessorFactoryProvider = scriptProcessorFactoryProvider;
        }

        public void Register(Container container)
        {
            container.Register<ITraversalContext, TraversalContext>();
            container.Register(() => _scriptProcessorFactoryProvider());
            container.Register(() => _scriptParserFactoryProvider());
            container.Register(() => _functionHandlersProvider);
            container.Register(() => _rootHandlerMappersProvider);
        }
    }
}

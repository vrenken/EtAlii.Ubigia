namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptsScaffolding : IScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;

        public ScriptsScaffolding(
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
        }

        public void Register(Container container)
        {
            container.Register<ITraversalScriptContext, TraversalScriptContext>();
            container.Register<IScriptProcessorFactory, ScriptProcessorFactory>();
            container.Register<IScriptParserFactory, ScriptParserFactory>();
            container.Register(() => _functionHandlersProvider);
            container.Register(() => _rootHandlerMappersProvider);
        }
    }
}

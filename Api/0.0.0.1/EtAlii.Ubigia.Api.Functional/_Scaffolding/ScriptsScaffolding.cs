namespace EtAlii.Ubigia.Api.Functional
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
            container.Register<IScriptProcessorFactory, ScriptProcessorFactory>();
            container.Register<IScriptParserFactory, ScriptParserFactory>();
            container.Register<IFunctionHandlersProvider>(() => _functionHandlersProvider);
            container.Register<IRootHandlerMappersProvider>(() => _rootHandlerMappersProvider);
        }
    }
}
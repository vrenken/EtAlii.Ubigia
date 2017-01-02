namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptsScaffolding : IScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;

        public ScriptsScaffolding(IFunctionHandlersProvider functionHandlersProvider)
        {
            _functionHandlersProvider = functionHandlersProvider;
        }

        public void Register(Container container)
        {
            container.Register<IScriptProcessorFactory, ScriptProcessorFactory>();
            container.Register<IScriptParserFactory, ScriptParserFactory>();
            container.Register<IFunctionHandlersProvider>(() => _functionHandlersProvider);
        }
    }
}
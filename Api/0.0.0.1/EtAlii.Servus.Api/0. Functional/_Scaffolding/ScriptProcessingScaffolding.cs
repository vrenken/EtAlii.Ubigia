namespace EtAlii.Servus.Api.Functional
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingScaffolding : IScaffolding
    {
        private readonly IFunctionHandlerConfiguration[] _functionHandlerConfigurations;

        public ScriptProcessingScaffolding(IFunctionHandlerConfiguration[] functionHandlerConfigurations)
        {
            _functionHandlerConfigurations = functionHandlerConfigurations;
        }

        public void Register(Container container)
        {
            container.Register<IScriptProcessorFactory, ScriptProcessorFactory>(Lifestyle.Singleton);
        }
    }
}
